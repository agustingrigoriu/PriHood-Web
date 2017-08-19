﻿using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Barrio
    {
        public Barrio()
        {
            Empleado = new HashSet<Empleado>();
            Residencia = new HashSet<Residencia>();
            UsuarioXbarrio = new HashSet<UsuarioXbarrio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Residencia> Residencia { get; set; }
        public virtual ICollection<UsuarioXbarrio> UsuarioXbarrio { get; set; }
    }
}
