using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNNatural:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNNatural()
        {
            esquema = this.traerContexto();
        }
    }
}
