using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Ventas.Pasaje.Datos
{
    public class Contexto
    {
        public VENTAS_PASAJE_ENTITIES traerContexto()
        {
            try
            {
                VENTAS_PASAJE_ENTITIES esquema = new VENTAS_PASAJE_ENTITIES();
                return esquema;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
