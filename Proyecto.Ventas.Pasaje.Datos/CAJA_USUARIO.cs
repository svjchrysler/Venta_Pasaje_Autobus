//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Ventas.Pasaje.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAJA_USUARIO
    {
        public long Id { get; set; }
        public Nullable<long> IdCaja { get; set; }
        public Nullable<long> IdUsuario { get; set; }
        public System.TimeSpan Hora { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public Nullable<int> Estado { get; set; }
        public System.DateTime Fecha { get; set; }
    
        public virtual CAJA CAJA { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}