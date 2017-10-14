using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Trayecto
    {
        public int Id { get; set; }
        public int IdViaje { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int Orden { get; set; }

        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
