using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class TipoAmenity
    {
        public TipoAmenity()
        {
            Amenity = new HashSet<Amenity>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Imagen { get; set; }

        public virtual ICollection<Amenity> Amenity { get; set; }
    }
}
