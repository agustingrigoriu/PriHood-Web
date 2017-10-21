using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class EstadoSolicitud
    {
        public EstadoSolicitud()
        {
            SolicitudViaje = new HashSet<SolicitudViaje>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SolicitudViaje> SolicitudViaje { get; set; }
    }
}
