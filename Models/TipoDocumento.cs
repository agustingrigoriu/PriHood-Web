using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Persona = new HashSet<Persona>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Persona> Persona { get; set; }
    }
}
