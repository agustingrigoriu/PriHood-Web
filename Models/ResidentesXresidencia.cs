using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class ResidentesXresidencia
    {
        public int IdResidencia { get; set; }
        public int IdResidente { get; set; }

        public virtual Residencia IdResidenciaNavigation { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
    }
}
