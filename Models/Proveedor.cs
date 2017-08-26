using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            RegistroVotos = new HashSet<RegistroVotos>();
        }

        public int Id { get; set; }
        public string Avatar { get; set; }
        public int CantidadVotos { get; set; }
        public string Descripcion { get; set; }
        public int IdResidenteRecomienda { get; set; }
        public int IdTipoServicio { get; set; }
        public string Nombre { get; set; }
        public float RatingPromedio { get; set; }
        public int RatingTotal { get; set; }

        public virtual ICollection<RegistroVotos> RegistroVotos { get; set; }
        public virtual Residente IdResidenteRecomiendaNavigation { get; set; }
        public virtual TipoServicio IdTipoServicioNavigation { get; set; }
    }
}
