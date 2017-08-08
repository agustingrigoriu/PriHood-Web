﻿using System;
using System.Collections.Generic;

namespace PriHood.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
