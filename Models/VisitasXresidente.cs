using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class VisitasXresidente
    {
        public int IdVisita { get; set; }
        public int IdResidente { get; set; }

        public virtual Residente IdResidenteNavigation { get; set; }
        public virtual Visita IdVisitaNavigation { get; set; }
    }
}
