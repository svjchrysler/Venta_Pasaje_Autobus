using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNDosificacion:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNDosificacion()
        {
            esquema = traerContexto();
        }

        public List<DOSIFICACION> traerDosificacion()
        {
            try
            {
                return (from f in esquema.DOSIFICACION select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Boolean actualizarDosificacion(DOSIFICACION dosificacion)
        {
            try
            {
                var info = esquema.DOSIFICACION.Find(dosificacion.Id);
                info.NroIncial = info.NroIncial + 1;
                return esquema.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
