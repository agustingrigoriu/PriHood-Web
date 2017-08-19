using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using PriHood.Auth;
using System.Security.Cryptography;
using System.Text;
using FluentEmail.Core;

namespace PriHood.Controllers
{
  public class ModeloLogin
  {
    public string email { get; set; }
    public string password { get; set; }
  }
  public class ModeloEmail
  {
    public string email { get; set; }
  }

  public class ModeloResidente : ModeloUsuario
  {
    public int id_residencia { get; set; }
  }

  public class ModeloEmpleado : ModeloUsuario
  {
    public int id_barrio { get; set; }
  }

  public class ModeloBarrioAdministrador
  {
    public ModeloEmpleado usuario { get; set; }
    public Barrio barrio { get; set; }
  }

  public class ModeloUsuario
  {
    public string nombre { get; set; }
    public string apellido { get; set; }
    public int id_tipo_documento { get; set; }
    public string numero_documento { get; set; }
    public string telefono { get; set; }
    public DateTime fecha_nacimiento { get; set; }
    public string email { get; set; }
    public string password { get; set; }
  }
}
