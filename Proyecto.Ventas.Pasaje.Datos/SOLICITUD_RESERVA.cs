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
    
    public partial class SOLICITUD_RESERVA
    {
        public SOLICITUD_RESERVA()
        {
            this.DETALLE_SOLICITUD_RESERVA = new HashSet<DETALLE_SOLICITUD_RESERVA>();
        }
    
        public long Id { get; set; }
        public Nullable<long> IdCliente { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<int> Estado { get; set; }
        public Nullable<double> MontoTotal { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual ICollection<DETALLE_SOLICITUD_RESERVA> DETALLE_SOLICITUD_RESERVA { get; set; }
    }
}