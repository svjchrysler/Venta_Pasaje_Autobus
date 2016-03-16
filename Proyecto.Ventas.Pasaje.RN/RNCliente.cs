using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNCliente:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNCliente()
        {
            esquema = this.traerContexto();
        }

        public Int64 generarId()
        {
            try
            {
                return (from f in esquema.CLIENTE select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<V_CLIENTE_NATURAL> verificarInformacionCI(Int32 CI)
        {
            try
            {
                return (from f in esquema.V_CLIENTE_NATURAL where f.CI == CI select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_CLIENTE_NATURAL> verificarInformacionNATURALNIT(Int32 NIT)
        {
            try
            {
                return (from f in esquema.V_CLIENTE_NATURAL where f.NIT == NIT select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_CLIENTE_JURIDICO> verificarInformacionJURIDICONIT(Int32 NIT)
        {
            try
            {
                return (from f in esquema.V_CLIENTE_JURIDICO where f.NIT == NIT select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_CLIENTE_NATURAL> traerClienteNatural(Int32 CI)
        {
            try
            {
                return (from f in esquema.V_CLIENTE_NATURAL where f.CI == CI select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
