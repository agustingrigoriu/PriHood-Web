using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Barrio
    {
        public Barrio()
        {
            Amenity = new HashSet<Amenity>();
            Empleado = new HashSet<Empleado>();
            Expensas = new HashSet<Expensas>();
            Residencia = new HashSet<Residencia>();
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<Amenity> Amenity { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Expensas> Expensas { get; set; }
        public virtual ICollection<Residencia> Residencia { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
