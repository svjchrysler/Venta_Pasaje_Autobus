using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNDetalleDepartamentoHorario:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNDetalleDepartamentoHorario()
        {
            esquema = this.traerContexto();
        }

        public Boolean registrar(DETALLE_DEPARTAMENTO_HORARIO detalleDepartamentoHorario)
        {
            try
            {
                esquema.DETALLE_DEPARTAMENTO_HORARIO.Add(detalleDepartamentoHorario);
                return (esquema.SaveChanges() == 1);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
