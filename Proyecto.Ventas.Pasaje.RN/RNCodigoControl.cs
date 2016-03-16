using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNCodigoControl
    {
        public String DevolverCodigoControl(double montoTransferencia,String nroAutorizacion,String nroFactura,String Nit,String dosificacion)
        {
            try
            {
                String na = nroAutorizacion;
                String nf = nroFactura;
                String NitCi = Nit;
                String ft = DateTime.Now.Year.ToString() + redondeo(DateTime.Now.Month) + redondeo(DateTime.Now.Day);
                String mt = Math.Round(montoTransferencia).ToString();
                String lld = dosificacion;

                String result = String.Empty;
                String dg = String.Empty;

                Int32 n = 0;

                String cc = String.Empty;
                String alleged = String.Empty;

                Int64 st = 0;

                Int64 sp1, sp2, sp3, sp4, sp5;
                sp1 = sp2 = sp3 = sp4 = sp5 = 0;


                //2 digitos verhoeff a numero de factura
                nf = verhoeff(verhoeff(nf));

                //2 digitos verhoeff a nit o ci
                NitCi = verhoeff(verhoeff(NitCi));

                //2 digitos verhoeff fecha de transaccion
                ft = verhoeff(verhoeff(ft));

                //2 digitos verhoeff a monto de la transaccion
                mt = verhoeff(verhoeff(mt));

                //Sumatoria
                result = (Int64.Parse(nf) + Int64.Parse(NitCi) + Int64.Parse(ft) + Int64.Parse(mt)).ToString();

                //5 digitos de verhoeff a la suma total
                result = verhoeff(verhoeff(verhoeff(verhoeff(verhoeff(result)))));

                //obtener 5 digitos de verhoesff
                dg = result.Substring(result.Length - 5, 5);

                lld += dg;

                //concatenar con la cantidad de digitos
                na = na + lld.Substring(n, Int32.Parse(dg.Substring(0, 1)) + 1);
                n += Int32.Parse(dg.Substring(0, 1)) + 1;

                //concatenar con la cantidad de digitos
                nf = nf + lld.Substring(n, Int32.Parse(dg.Substring(1, 1)) + 1);
                n += Int32.Parse(dg.Substring(1, 1)) + 1;

                //concatenar con la cantidad de digitos
                NitCi = NitCi + lld.Substring(n, Int32.Parse(dg.Substring(2, 1)) + 1);
                n += Int32.Parse(dg.Substring(2, 1)) + 1;

                //concatenar con la cantidad de digitos
                ft = ft + lld.Substring(n, Int32.Parse(dg.Substring(3, 1)) + 1);
                n += Int32.Parse(dg.Substring(3, 1)) + 1;

                //concatenar con la cantidad de digitos
                mt = mt + lld.Substring(n, Int32.Parse(dg.Substring(4, 1)) + 1);

                //concatenar todos los valores
                cc = na + nf + NitCi + ft + mt;

                //calcular el allegedRC4 con la concatenacion de todos los valores y la llave de dosificacion
                alleged = AllegedRC4.allegedrc4(cc, lld);

                //sumar el codigo ASCII de todos los caracteres obtenidos de alleged
                st = sumatoriaASCII(alleged);

                for (int i = 0; i < alleged.Length; i++)
                {
                    sp1 += Encoding.ASCII.GetBytes(alleged[i].ToString())[0];
                    if (i + 1 < alleged.Length)
                        sp2 += Encoding.ASCII.GetBytes(alleged[i + 1].ToString())[0];
                    if (i + 2 < alleged.Length)
                        sp3 += Encoding.ASCII.GetBytes(alleged[i + 2].ToString())[0];
                    if (i + 3 < alleged.Length)
                        sp4 += Encoding.ASCII.GetBytes(alleged[i + 3].ToString())[0];
                    if (i + 4 < alleged.Length)
                        sp5 += Encoding.ASCII.GetBytes(alleged[i + 4].ToString())[0];
                    i += 4;
                }

                Int64 st1, st2, st3, st4, st5;

                st1 = (st * sp1) / (Int64.Parse(dg.Substring(0, 1)) + 1);
                st2 = (st * sp2) / (Int64.Parse(dg.Substring(1, 1)) + 1);
                st3 = (st * sp3) / (Int64.Parse(dg.Substring(2, 1)) + 1);
                st4 = (st * sp4) / (Int64.Parse(dg.Substring(3, 1)) + 1);
                st5 = (st * sp5) / (Int64.Parse(dg.Substring(4, 1)) + 1);

                Int64 stp = st1 + st2 + st3 + st4 + st5;

                String base64 = Base64.base64(stp);

                String codControl = AllegedRC4.allegedrc4(base64, lld);

                return separar(codControl);
            }
            catch (Exception)
            {
                return "D8-DD-A5-9A";
            }
        }

        private String separar(String cc)
        {
            String cad = "";
            for (int i = 1; i <= cc.Length; i++)
            {
                if (i % 2 == 0 && i != cc.Length)
                    cad += cc[i - 1].ToString() + "-";
                else
                    cad += cc[i - 1].ToString();
            }
            return cad;
        }

        private String redondeo(int n)
        {
            if (n > 9)
                return n.ToString();
            else
                return "0" + n.ToString();
        }

        private Int64 sumatoriaASCII(String cc)
        {
            Int64 st = 0;
            for (int i = 0; i < cc.Length; i++)
            {
                st += Encoding.ASCII.GetBytes(cc[i].ToString())[0];
            }
            return st;
        }

        private String verhoeff(String num)
        {
            num = num + Verhoeff.generateVerhoeff(num);
            return num;
        }
    
    }
}
