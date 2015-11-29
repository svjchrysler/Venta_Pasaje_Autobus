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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IWebService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IWebService
    {
        [OperationContract]
        List<V_VENTA_EMPLEADO> traerVentaEmpleado(DateTime desde,DateTime hasta);
    }
}
