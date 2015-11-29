using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNDepartamento:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNDepartamento()
        {
            esquema = this.traerContexto();
        }

        public List<DEPARTAMENTO> traerDepartamento(Int64 IdDepartamento)
        {
            try
            {
                return (IdDepartamento.Equals(0)) ? (from f in esquema.DEPARTAMENTO select f).ToList() : (from f in esquema.DEPARTAMENTO where f.Id.Equals(IdDepartamento) select f).ToList();
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
    }
}
