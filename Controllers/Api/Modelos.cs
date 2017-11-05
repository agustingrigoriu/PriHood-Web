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
using Microsoft.AspNetCore.Http;

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
    public int? id_barrio { get; set; }
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
    public int? id_perfil { get; set; }
    public IFormFile avatar {get; set;}
  }

  public class ModeloVisitante
  {
    public string nombre { get; set; }
    public string apellido { get; set; }
    public int id_tipo_documento { get; set; }
    public int id_tipo_visita { get; set; }
    public string numero_documento { get; set; }
    public DateTime? fecha_visita { get; set; }
    public string avatar { get; set; }
    public string observaciones { get; set; }
    public string patente { get; set; }
  }

  public class ModeloToken
  {
    public string token { get; set; }
  }

  public class ModeloProveedor
  {
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public string telefono { get; set; }
    public int id_tipo_servicio { get; set; }
    public string avatar { get; set; }
    public string direccion { get; set; }

  }

  public class ModeloVoto
  {
    public int rating { get; set; }
    public string comentario { get; set; }

  }

  public class ModeloTurnoUpdate
  {
    public float? Costo { get; set; }
    public int? Duracion { get; set; }
    public TimeSpan? HoraDesde { get; set; }
    public int? IdDiaSemana { get; set; }
    public string Nombre { get; set; }
  }

  public class ModeloEventosUpdate
  {
    public string Descripcion { get; set; }
    public int? Duracion { get; set; }
    public DateTime? Fecha { get; set; }
    public TimeSpan? HoraDesde { get; set; }
    public int? IdResidente { get; set; }
    public int? IdTipoEvento { get; set; }
    public string Imagen { get; set; }
  }

  public class ModeloPassword
  {
    public string email { get; set; }
  }

  public class ModeloChangePassword
  {
    public string password_actual { get; set; }
    public string password_nueva { get; set; }
  }

  public class ModeloExpensas
  {
    public int id { get; set; }
    public int id_residencia { get; set; }
    public int id_barrio { get; set; }
    public DateTime fecha_expensa { get; set; }
    public DateTime? fecha_vencimiento { get; set; }
    public float monto { get; set; }
    public bool? pagado { get; set; }
    public string observaciones { get; set; }
    public IFormFile file { get; set; }
  }

  public class ModeloCarpooling
  {
    public Viaje viaje { get; set; }
    public List<Trayecto> trayectos { get; set; }
  }

  public class ModeloEstadoSolicitud
  {
    public string estado_solicitud { get; set; }
  }
  
}
