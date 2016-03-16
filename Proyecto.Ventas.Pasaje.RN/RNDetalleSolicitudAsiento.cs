using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;
 
namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNDetalleSolicitudAsiento:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNDetalleSolicitudAsiento()
        {
            esquema = traerContexto();
        }

        public Boolean registrar(DETALLE_SOLICITUD_ASIENTO detalleSolicitudAsiento)
        {
            try
            {
                esquema.DETALLE_SOLICITUD_ASIENTO.Add(detalleSolicitudAsiento);
                return esquema.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
