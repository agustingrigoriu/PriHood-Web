using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class EventoVisita
    {
        public EventoVisita()
        {
            Visita = new HashSet<Visita>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Visita> Visita { get; set; }
    }
}
