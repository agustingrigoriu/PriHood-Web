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

    [HttpGet("{id_empleado?}")]
    public Object ListarEmpleados(int? id_empleado)
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
          where e.IdBarrio == id_barrio && ((id_empleado.HasValue && e.Id == id_empleado.Value) || !id_empleado.HasValue)
          select new
          {
            id_empleado = e.Id,
            apellido = p.Apellido,
            nombre = p.Nombre,
            fecha_nacimiento = p.FechaNacimiento,
            numero_documento = p.NroDocumento,
            telefono = p.TelefonoMovil,
            perfil = pf.Descripcion,
            tipo_documento = td.Descripcion,
            email = u.Email,
            id_perfil = pf.Id,
            id_tipo_documento = td.Id
          }
        ).ToList();

        if (id_empleado.HasValue)
        {
          return new { error = false, data = empleados.First() };
        }

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
          usuario.IdBarrio = logueado.IdBarrio.Value;
          db.Usuario.Add(usuario);

          db.SaveChanges();

          var empleado = new Empleado();
          empleado.FechaInicioActividad = DateTime.Now;
          empleado.IdBarrio = logueado.IdBarrio.Value;
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

    [HttpPut("{id_empleado}")]
    public Object ActualizarEmpleado(int id_empleado, [FromBody]ModeloEmpleado me)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {

          var empleado = db.Empleado.FirstOrDefault(e => e.Id == id_empleado);
          if (empleado == null) return new { error = true, data = "No existe ese empleado" };

          var persona = new Persona();
          persona.Id = empleado.IdPersona;
          persona.Apellido = me.apellido;
          persona.Nombre = me.nombre;
          persona.FechaNacimiento = me.fecha_nacimiento;
          persona.IdTipoDocumento = me.id_tipo_documento;
          persona.NroDocumento = me.numero_documento;
          persona.TelefonoMovil = me.telefono;

          db.Persona.Update(persona);
          db.SaveChanges();

          var usuario = (
            from u in db.Usuario
            where u.Id == empleado.IdUsuario
            select u
          ).First();

          usuario.Id = empleado.IdUsuario;
          usuario.Email = me.email;
          if (me.password != null && me.password != "") usuario.Password = auth.getHash(me.password);
          usuario.IdPerfil = me.id_perfil.Value;
          db.Usuario.Update(usuario);
          db.SaveChanges();

          transaction.Commit();
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", err.Message };
        }
      }

      return new { error = false, data = "ok" };
    }

    [HttpDelete("{id_empleado}")]
    public Object EliminarEmpleado(int id_empleado)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var id_barrio = logueado.IdBarrio;

          if (db.Empleado.Where(a => a.Id == id_empleado).Count() > 0)
          {

            var empleado = db.Empleado.First(t => t.Id == id_empleado);
            var usuario = db.Usuario.First(u => u.Id == empleado.IdUsuario);
            var persona = db.Persona.First(p => p.Id == empleado.IdPersona);
            db.Empleado.Remove(empleado);
            db.Usuario.Remove(usuario);
            db.Persona.Remove(persona);
            db.SaveChanges();
            transaction.Commit();
            return new { error = false, data = empleado };
          }

          return new { error = false, data = "No existe ese empleado" };

        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "fail", message = err.Message };
        }
      }
    }
  }
}
