using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Muro
    {
        public Muro()
        {
            Publicacion = new HashSet<Publicacion>();
        }

        public int Id { get; set; }
        public int IdBarrio { get; set; }

        public virtual ICollection<Publicacion> Publicacion { get; set; }
        public virtual Barrio IdBarrioNavigation { get; set; }
    }
}
