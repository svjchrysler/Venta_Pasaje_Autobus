using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;

namespace Proyecto.Ventas.Pasaje.WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "WebService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione WebService.svc o WebService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WebService : IWebService
    {
        public List<V_VENTA_EMPLEADO> traerVentaEmpleado(DateTime desde, DateTime hasta)
        {
            RNSolicitudPasaje negocioSolicitud = new RNSolicitudPasaje();
            return negocioSolicitud.traerVentaEmpleado(desde, hasta);
        }
    }
}
