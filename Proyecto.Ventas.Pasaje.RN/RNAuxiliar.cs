using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNAuxiliar:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNAuxiliar()
        {
            esquema = this.traerContexto();
        }

        public Boolean actualizarPrecio(AUXILIAR auxiliar)
        {
            try
            {
                var info = esquema.AUXILIAR.Find(auxiliar.Id);
                info.PrecioMaximo = auxiliar.PrecioMaximo;
                info.PrecioMinimo = auxiliar.PrecioMinimo;
                return esquema.SaveChanges() == 1;
                
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                esquema.Dispose();
            }
        }

        public AUXILIAR traerAuxiliar()
        {
            try
            {
                return (from f in esquema.AUXILIAR select f).ToList()[0];
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
