using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class UsuarioXbarrio
    {
        public int IdBarrio { get; set; }
        public int IdUsuario { get; set; }

        public virtual Barrio IdBarrioNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
