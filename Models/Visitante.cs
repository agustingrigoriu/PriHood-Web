using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Visitante
    {
        public Visitante()
        {
            Visita = new HashSet<Visita>();
        }

        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Avatar { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaVisita { get; set; }
        public int IdResidente { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int IdTipoVisita { get; set; }
        public string Nombre { get; set; }
        public string NumeroDocumento { get; set; }
        public string Observaciones { get; set; }
        public string Patente { get; set; }

        public virtual ICollection<Visita> Visita { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
        public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }
        public virtual TipoVisita IdTipoVisitaNavigation { get; set; }
    }
}
