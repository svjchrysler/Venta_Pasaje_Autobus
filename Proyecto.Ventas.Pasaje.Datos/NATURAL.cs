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
    
    public partial class NATURAL
    {
        public long Id { get; set; }
        public Nullable<long> CI { get; set; }
        public string NombreCompleto { get; set; }
    
        public virtual CLIENTE CLIENTE { get; set; }
    }
}
