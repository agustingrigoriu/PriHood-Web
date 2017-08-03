using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Empleado = new HashSet<Empleado>();
            Residente = new HashSet<Residente>();
            UsuarioXbarrio = new HashSet<UsuarioXbarrio>();
        }

        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public int IdPerfil { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Residente> Residente { get; set; }
        public virtual ICollection<UsuarioXbarrio> UsuarioXbarrio { get; set; }
        public virtual Perfil IdPerfilNavigation { get; set; }
    }
}
