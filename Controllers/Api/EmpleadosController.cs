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
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class EmpleadosController : Controller
  {
    private readonly PrihoodContext db;
    private AuthService auth;
    public EmpleadosController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
    }

    [HttpGet]
    public Object ListarEmpleados()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var empleados = (
          from e in db.Empleado
          join p in db.Persona on e.IdPersona equals p.Id
          join u in db.Usuario on e.IdUsuario equals u.Id
          join pf in db.Perfil on u.IdPerfil equals pf.Id
          join td in db.TipoDocumento on p.IdTipoDocumento equals td.Id
          where e.IdBarrio == id_barrio
          select new
          {
            apellido = p.Apellido,
            nombre = p.Nombre,
            fecha_nacimiento = p.FechaNacimiento,
            numero_documento = p.NroDocumento,
            telefono = p.TelefonoMovil,
            perfil = pf.Descripcion,
            tipo_documento = td.Descripcion,
            email = u.Email
          }
        ).ToList();

        return new { error = false, data = empleados };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpPost]
    public Object AgregarEmpleado([FromBody]ModeloEmpleado me)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var persona = new Persona();
          persona.Apellido = me.apellido;
          persona.Nombre = me.nombre;
          persona.FechaNacimiento = me.fecha_nacimiento;
          persona.IdTipoDocumento = me.id_tipo_documento;
          persona.NroDocumento = me.numero_documento;
          persona.TelefonoMovil = me.telefono;
          db.Persona.Add(persona);

          db.SaveChanges();

          var usuario = new Usuario();

          usuario.Email = me.email;
          usuario.Password = auth.getHash(me.password); // hasheo le password
          usuario.IdPerfil = me.id_perfil.Value;
          db.Usuario.Add(usuario);

          db.SaveChanges();

          var empleado = new Empleado();
          empleado.FechaInicioActividad = DateTime.Now;
          empleado.IdBarrio = logueado.IdBarrio.Value; // @TODO: warning.. hardcode in progress.. fixme!!
          empleado.IdPersona = persona.Id;
          empleado.IdUsuario = usuario.Id;
          db.Empleado.Add(empleado);

          db.SaveChanges();

          transaction.Commit();
        }
        catch (Exception)
        {
          transaction.Rollback();
          return new { error = true, data = "Error" };
        }
      }

      return new { error = false, data = "ok" };
    }
  }
}
