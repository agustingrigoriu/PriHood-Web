using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoEmpleado
    {
        public TipoEmpleado()
        {
            Empleado = new HashSet<Empleado>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
