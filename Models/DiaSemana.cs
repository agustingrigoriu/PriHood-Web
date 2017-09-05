using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class DiaSemana
    {
        public DiaSemana()
        {
            Turno = new HashSet<Turno>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Turno> Turno { get; set; }
    }
}
