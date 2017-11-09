using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Eventos
    {
        public Eventos()
        {
            AsistenciaEvento = new HashSet<AsistenciaEvento>();
            ComentariosEvento = new HashSet<ComentariosEvento>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraDesde { get; set; }
        public int IdResidente { get; set; }
        public int IdTipoEvento { get; set; }
        public string Imagen { get; set; }

        public virtual ICollection<AsistenciaEvento> AsistenciaEvento { get; set; }
        public virtual ICollection<ComentariosEvento> ComentariosEvento { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
        public virtual TipoEvento IdTipoEventoNavigation { get; set; }
    }
}
