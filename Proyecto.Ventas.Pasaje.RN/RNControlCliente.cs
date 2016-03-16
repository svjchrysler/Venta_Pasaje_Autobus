using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNControlCliente:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNControlCliente()
        {
            esquema = traerContexto();
        }

        public Boolean registrarClienteNatural(CLIENTE cliente,NATURAL natural)
        {
            using (TransactionScope transaccion = new TransactionScope())
            {
                try
                {
                    esquema.CLIENTE.Add(cliente);
                    esquema.NATURAL.Add(natural);
                    int valor = esquema.SaveChanges();
                    transaccion.Complete();
                    return (valor == 2);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Boolean registrarClienteJuridico(CLIENTE cliente, JURIDICO juridico)
        {
            using (TransactionScope transaccion = new TransactionScope())
            {
                try
                {
                    esquema.CLIENTE.Add(cliente);
                    esquema.JURIDICO.Add(juridico);
                    int valor = esquema.SaveChanges();
                    transaccion.Complete();
                    return (valor == 2);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
