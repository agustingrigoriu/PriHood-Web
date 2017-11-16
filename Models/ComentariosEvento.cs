using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class ComentariosEvento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEvento { get; set; }
        public int IdUsuario { get; set; }
        public string Texto { get; set; }

        public virtual Eventos IdEventoNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
