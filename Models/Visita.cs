using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Visita
    {
        public Visita()
        {
            VisitasXresidente = new HashSet<VisitasXresidente>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEvento { get; set; }
        public int IdVisitante { get; set; }

        public virtual ICollection<VisitasXresidente> VisitasXresidente { get; set; }
        public virtual EventoVisita IdEventoNavigation { get; set; }
        public virtual Visitante IdVisitanteNavigation { get; set; }
    }
}
