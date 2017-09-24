using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Publicacion
    {
        public Publicacion()
        {
            Comentario = new HashSet<Comentario>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPersonal { get; set; }
        public int? IdResidente { get; set; }
        public string Texto { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual Usuario IdPersonalNavigation { get; set; }
        public virtual Usuario IdResidenteNavigation { get; set; }
    }
}
