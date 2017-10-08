using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoEvento
    {
        public TipoEvento()
        {
            Eventos = new HashSet<Eventos>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Eventos> Eventos { get; set; }
    }
}
