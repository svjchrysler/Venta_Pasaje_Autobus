using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Ventas.Pasaje.RN
{
    class AllegedRC4
    {
        public static string allegedrc4(string codigo, string llavellegada)
        {
            int[] State = new int[256 + 1];
            string Mensaje = String.Empty;
            string llave = String.Empty;
            string MsgCif = String.Empty;
            int X = 0;
            int Y = 0;
            int Index1 = 0;
            int Index2 = 0;
            int NMen = 0;
            int i = 0;
            short op1 = 0;
            int aux = 0;
            int op2 = 0;
            string nrohex = String.Empty;


            X = 0;
            Y = 0;
            Index1 = 0;
            Index2 = 0;
            Mensaje = codigo;
            llave = llavellegada;
            for (i = 0; i <= 255.0; i += 1)
            {
                State[i] = i;
            }
            for (i = 0; i <= 255.0; i += 1)
            {
                op1 = (short)(llave.Substring(Index1 + 1 - 1, 1)[0]);
                Index2 = (op1 + State[i] + Index2) % 256;
                aux = State[i];
                State[i] = State[Index2];
                State[Index2] = aux;
                Index1 = (Index1 + 1) % llave.Length;
            }
            for (i = 0; i <= Mensaje.Length - 1; i += 1)
            {
                X = (X + 1) % 256;
                Y = (State[X] + Y) % 256;
                aux = State[X];
                State[X] = State[Y];
                State[Y] = aux;
                op1 = (short)(Mensaje.Substring(i + 1 - 1, 1)[0]);
                op2 = State[(State[X] + State[Y]) % 256];
                NMen = op1 ^ op2;
                nrohex = NMen.ToString("X");
                if (nrohex.Length == 1)
                {
                    nrohex = "0" + nrohex;
                }
                MsgCif = MsgCif + nrohex;
            }
            MsgCif = MsgCif.Substring(MsgCif.Length - (MsgCif.Length));
            return MsgCif;
        }
    }
}
