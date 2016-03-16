using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNHorario:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNHorario()
        {
            esquema = this.traerContexto();
        }

        public Int64 generarId()
        {
            try
            {
                return (from f in esquema.HORARIO select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<HORARIO> traerHorario(TimeSpan hora)
        {
            try
            {
                return (from f in esquema.HORARIO where f.Hora.Equals(hora) select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_INFORMACION_HORARIO> traerHorarioDepartamento(Int64 IdDepartamento)
        {
            try
            {
                return (from f in esquema.V_INFORMACION_HORARIO where f.IdDepartamento.Equals(IdDepartamento) orderby f.Hora ascending select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                esquema.Dispose();
            }
        }

        public Boolean registrar(HORARIO horario)
        {
            try
            {
                if ((from f in esquema.HORARIO where f.Hora.Equals(horario.Hora) select f).ToList().Count > 0)
                    return false;
                else
                {
                    esquema.HORARIO.Add(horario);
                    return (esquema.SaveChanges() == 1);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
