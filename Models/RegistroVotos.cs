using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class RegistroVotos
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdProveedor { get; set; }
        public int IdResidente { get; set; }
        public int Rating { get; set; }

        public virtual Proveedor IdProveedorNavigation { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
    }
}
