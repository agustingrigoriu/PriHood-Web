using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Comentario
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public string Texto { get; set; }

        public virtual Publicacion IdPublicacionNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
