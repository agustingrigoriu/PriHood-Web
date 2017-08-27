using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoServicio
    {
        public TipoServicio()
        {
            Proveedor = new HashSet<Proveedor>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
