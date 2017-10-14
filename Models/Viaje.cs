using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Viaje
    {
        public Viaje()
        {
            SolicitudViaje = new HashSet<SolicitudViaje>();
            Trayecto = new HashSet<Trayecto>();
        }

        public int Id { get; set; }
        public int AutoAsientos { get; set; }
        public string AutoColor { get; set; }
        public string AutoModelo { get; set; }
        public string AutoPatente { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdDiaSemana { get; set; }
        public int IdResidente { get; set; }
        public string Observaciones { get; set; }
        public bool SaleBarrio { get; set; }

        public virtual ICollection<SolicitudViaje> SolicitudViaje { get; set; }
        public virtual ICollection<Trayecto> Trayecto { get; set; }
        public virtual DiaSemana IdDiaSemanaNavigation { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
    }
}
