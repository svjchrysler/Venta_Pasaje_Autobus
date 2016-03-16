using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNUsuario:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNUsuario()
        {
            esquema = traerContexto();
        }

        public List<V_EMPLEADO_USUARIO> validarLogin(String username, String password)
        {
            try
            {
                return (from f in esquema.V_EMPLEADO_USUARIO where f.Username == username && f.Password == password select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
