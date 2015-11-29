using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNControlVentaPasaje:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNControlVentaPasaje()
        {
            esquema = traerContexto();
        }

        public Boolean registroVentaFactura(SOLICITUD_PASAJE solicitudPasaje,FACTURA factura,List<DETALLE_SOLICITUD_ASIENTO> listDetalleSolicitudAsiento)
        {
            using (TransactionScope transaccion = new TransactionScope())
            {
                try
                {

                    esquema.SOLICITUD_PASAJE.Add(solicitudPasaje);

                    foreach (var item in listDetalleSolicitudAsiento)
                    {
                        esquema.DETALLE_SOLICITUD_ASIENTO.Add(item);
                    }

                    esquema.FACTURA.Add(factura);

                    int valor = esquema.SaveChanges();
                    transaccion.Complete();
                    return valor >= 3;

                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    transaccion.Dispose();
                    esquema.Dispose();
                }
            }
        }  
   
    }
}
