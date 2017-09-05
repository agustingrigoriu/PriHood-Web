using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Reserva
    {
        public int Id { get; set; }
        public float Costo { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstadoReserva { get; set; }
        public int IdResidente { get; set; }
        public int IdTurno { get; set; }
        public string Observaciones { get; set; }

        public virtual EstadoReserva IdEstadoReservaNavigation { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
        public virtual Turno IdTurnoNavigation { get; set; }
    }
}
