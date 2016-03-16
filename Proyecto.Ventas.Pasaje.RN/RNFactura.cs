using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNFactura:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNFactura()
        {
            esquema = traerContexto();
        }

        public Int64 generarId()
        {
            try
            {
                return (from f in esquema.FACTURA select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<V_FACTURA> traerFactura(Int64 IdFactura)
        {
            try
            {
                return (from f in esquema.V_FACTURA where f.IdFactura == IdFactura select f).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean registrar(FACTURA factura)
        {
            try
            {
                esquema.FACTURA.Add(factura);
                return esquema.SaveChanges() == 1;
                     
            }
            catch (Exception) 
            {
                return false;
            }
        }
    }
}
