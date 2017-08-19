using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public int IdBarrio { get; set; }
        public int IdPersona { get; set; }
        public int IdUsuario { get; set; }

        public virtual Barrio IdBarrioNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
