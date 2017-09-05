using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Turno
    {
        public Turno()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int Id { get; set; }
        public float Costo { get; set; }
        public int Duracion { get; set; }
        public TimeSpan HoraDesde { get; set; }
        public int IdAmenity { get; set; }
        public int IdDiaSemana { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
        public virtual Amenity IdAmenityNavigation { get; set; }
        public virtual DiaSemana IdDiaSemanaNavigation { get; set; }
    }
}
