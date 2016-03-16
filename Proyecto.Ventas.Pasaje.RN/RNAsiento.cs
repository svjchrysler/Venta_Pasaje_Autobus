using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNAsiento:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNAsiento()
        {
            esquema = this.traerContexto();
        }

        public Int64 generarId()
        {
            try
            {
                return (from f in esquema.ASIENTO select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<ASIENTO> traerAsiento(String asiento,Int64 IdBus)
        {
            try
            {
                return (from f in esquema.ASIENTO where f.Asiento1.Trim() == asiento.Trim() && f.IdBus == IdBus select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Boolean registrar(ASIENTO asiento)
        {
            try
            {
                asiento.Id = generarId();
                esquema.ASIENTO.Add(asiento);
                return (esquema.SaveChanges()>=1) ;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}
