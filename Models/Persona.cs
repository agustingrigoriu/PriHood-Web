using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Empleado = new HashSet<Empleado>();
            Residente = new HashSet<Residente>();
        }

        public int Id { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; }
        public string NroDocumento { get; set; }
        public string TelefonoMovil { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Residente> Residente { get; set; }
        public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }
    }
}
