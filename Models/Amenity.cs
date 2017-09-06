using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Amenity
    {
        public Amenity()
        {
            Turno = new HashSet<Turno>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdBarrio { get; set; }
        public int IdTipoAmenity { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<Turno> Turno { get; set; }
        public virtual Barrio IdBarrioNavigation { get; set; }
        public virtual TipoAmenity IdTipoAmenityNavigation { get; set; }
    }
}
