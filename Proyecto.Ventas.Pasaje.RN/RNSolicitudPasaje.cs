using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNSolicitudPasaje:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNSolicitudPasaje()
        {
            esquema = traerContexto();
        }

        public Int64 generarId()
        {
            try
            {
                return (from f in esquema.SOLICITUD_PASAJE select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<V_VENTA_EMPLEADO> traerVentaEmpleado(DateTime desde,DateTime hasta)
        {
            try
            {
                return (from f in esquema.V_VENTA_EMPLEADO where f.Fecha >= desde && f.Fecha <= hasta select f).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<V_DETALLE_VIAJE> traerDetalleViaje(DateTime desde,DateTime hasta)
        {
            try
            {
                return (from f in esquema.V_DETALLE_VIAJE where f.Fecha >= desde && f.Fecha <= hasta select f).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean registrar(SOLICITUD_PASAJE solicitudPasaje)
        {
            try
            {
                esquema.SOLICITUD_PASAJE.Add(solicitudPasaje);
                return esquema.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
