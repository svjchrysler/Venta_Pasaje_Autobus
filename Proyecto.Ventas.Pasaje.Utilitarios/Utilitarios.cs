using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;

namespace Proyecto.Ventas.Pasaje.Utilitarios
{
    public class Utilitarios
    {
        public static Int64 IdFactura;
        public static String ruta = @"C:\Users\SALGUERO\Documents\Visual Studio 2015\Projects\Proyecto.Ventas.Pasaje.WPF\Proyecto de informe1\";

        public static Int64 IdCaja = 1;
        public static String Username;
        public static String NombreEmpleado;

        public static Int32 TipoFacturacion;
        public static List<INFORMACION_PASAJE> informacionAuxiliar;

        public static List<String> pasajesAgregados;
        public static Int64 idItinerario;
        public static Int64 idBus;


        public static Int64 idHorario;
        public static Int64 idEmpleado = 1;
        public static Int64 idDepartamento;


        public static Int64 idAuxiliar;
        public static Decimal? precioMaximo;
        public static Decimal? precioMinimo;

        public static String direccion = @"D:\Recursos\";
        public static String direccionEnumerada = @"D:\Recursos\Modificadas\";
    }
}
