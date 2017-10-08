using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Alertas
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int IdResidente { get; set; }
        public int IdTipoAlerta { get; set; }

        public virtual Residente IdResidenteNavigation { get; set; }
        public virtual TipoAlerta IdTipoAlertaNavigation { get; set; }
    }
}
