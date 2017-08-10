using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Residencia
    {
        public Residencia()
        {
            Residente = new HashSet<Residente>();
            ResidentesXresidencia = new HashSet<ResidentesXresidencia>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<Residente> Residente { get; set; }
        public virtual ICollection<ResidentesXresidencia> ResidentesXresidencia { get; set; }
    }
}
