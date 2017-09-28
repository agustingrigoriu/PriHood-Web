using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Expensas
    {
        public int Id { get; set; }
        public DateTime FechaExpensa { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int IdResidencia { get; set; }
        public float Monto { get; set; }
        public string Observaciones { get; set; }
        public bool? Pagado { get; set; }
        public string UrlExpensa { get; set; }

        public virtual Residencia IdResidenciaNavigation { get; set; }
    }
}
