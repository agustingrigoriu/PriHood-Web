﻿using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class SolicitudViaje
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public int IdResidente { get; set; }
        public int IdViaje { get; set; }

        public virtual EstadoSolicitud IdEstadoSolicitudNavigation { get; set; }
        public virtual Residente IdResidenteNavigation { get; set; }
        public virtual Viaje IdViajeNavigation { get; set; }
    }
}
