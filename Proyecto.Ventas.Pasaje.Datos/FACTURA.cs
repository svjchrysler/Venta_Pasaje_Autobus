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
    
    public partial class FACTURA
    {
        public long Id { get; set; }
        public Nullable<long> IdSolicitudPasaje { get; set; }
        public Nullable<long> IdDosificacion { get; set; }
        public Nullable<int> NroFactura { get; set; }
        public string CodigoControl { get; set; }
        public byte[] QR { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
    
        public virtual DOSIFICACION DOSIFICACION { get; set; }
        public virtual SOLICITUD_PASAJE SOLICITUD_PASAJE { get; set; }
    }
}
