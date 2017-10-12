using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoAlerta
    {
        public TipoAlerta()
        {
            Alertas = new HashSet<Alertas>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        public virtual ICollection<Alertas> Alertas { get; set; }
    }
}
