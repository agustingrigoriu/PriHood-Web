using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Barrio
    {
        public Barrio()
        {
            UsuarioXbarrio = new HashSet<UsuarioXbarrio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<UsuarioXbarrio> UsuarioXbarrio { get; set; }
    }
}
