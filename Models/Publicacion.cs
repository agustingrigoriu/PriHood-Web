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
        public TimeSpan Hora { get; set; }
        public int IdMuro { get; set; }
        public int IdUsuario { get; set; }
        public string Publicacion1 { get; set; }

        public virtual ICollection<Comentario> Comentario { get; set; }
        public virtual Muro IdMuroNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
