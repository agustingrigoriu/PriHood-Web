using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoVisita
    {
        public TipoVisita()
        {
            Visitante = new HashSet<Visitante>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Visitante> Visitante { get; set; }
    }
}
