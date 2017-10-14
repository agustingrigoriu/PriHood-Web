using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class DiaSemana
    {
        public DiaSemana()
        {
            Turno = new HashSet<Turno>();
            Viaje = new HashSet<Viaje>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Turno> Turno { get; set; }
        public virtual ICollection<Viaje> Viaje { get; set; }
    }
}
