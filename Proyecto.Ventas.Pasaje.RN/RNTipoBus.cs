
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNTipoBus:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNTipoBus()
        {
            try
            {
                esquema = this.traerContexto();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private Int64 generarId()
        {
            try
            {
                return (from f in esquema.TIPO_BUS select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public String registrar(TIPO_BUS tipoBus)
        {
            try
            {
                tipoBus.Id = generarId();
                esquema.TIPO_BUS.Add(tipoBus);
                return (esquema.SaveChanges() == 1) ? "Tipo de Bus registrado" : "El Tipo de Bus no pudo ser registrado";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                esquema.Dispose();
            }
        }

        public List<TIPO_BUS> traerTipoBus()
        {
            try
            {
                return (from f in esquema.TIPO_BUS select f).ToList();
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
