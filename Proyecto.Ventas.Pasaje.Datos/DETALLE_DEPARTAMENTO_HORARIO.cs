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
    
    public partial class DETALLE_DEPARTAMENTO_HORARIO
    {
        public DETALLE_DEPARTAMENTO_HORARIO()
        {
            this.ITINERARIO = new HashSet<ITINERARIO>();
        }
    
        public long IdDepartamento { get; set; }
        public long IdHorario { get; set; }
    
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual HORARIO HORARIO { get; set; }
        public virtual ICollection<ITINERARIO> ITINERARIO { get; set; }
    }
}
