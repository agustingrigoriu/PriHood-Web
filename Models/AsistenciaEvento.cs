using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class AsistenciaEvento
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public int IdResidente { get; set; }

        public virtual Eventos IdEventoNavigation { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
    }
}
