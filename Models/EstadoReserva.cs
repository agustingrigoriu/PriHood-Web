using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class EstadoReserva
    {
        public EstadoReserva()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
