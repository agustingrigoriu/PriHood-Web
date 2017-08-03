using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoEmpleado { get; set; }
        public int IdUsuario { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual TipoEmpleado IdTipoEmpleadoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
