using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Residente
    {
        public Residente()
        {
            ResidentesXresidencia = new HashSet<ResidentesXresidencia>();
        }

        public int Id { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int IdPersona { get; set; }
        public int IdResidencia { get; set; }
        public int IdUsuario { get; set; }

        public virtual ICollection<ResidentesXresidencia> ResidentesXresidencia { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Residencia IdResidenciaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
