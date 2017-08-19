using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using PriHood.Auth;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class BarriosController : Controller
  {

    private readonly PrihoodContext db;
    private AuthService auth;
    public BarriosController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
    }


    [HttpGet]
    public Object Get()
    {
      return new { error = false, data = db.Barrio.ToList() };
    }


    [HttpGet("{id}")]
    public Object Get(int id)
    {
      return new { error = false, data = db.Barrio.FirstOrDefault(u => u.Id == id) };
    }

    [HttpPost]
    public Object Post([FromBody]ModeloBarrioAdministrador mba)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          db.Barrio.Add(mba.barrio);

          var persona = new Persona();
          persona.Apellido = mba.usuario.apellido;
          persona.Nombre = mba.usuario.nombre;
          persona.FechaNacimiento = mba.usuario.fecha_nacimiento;
          persona.IdTipoDocumento = mba.usuario.id_tipo_documento;
          persona.NroDocumento = mba.usuario.numero_documento;
          persona.TelefonoMovil = mba.usuario.telefono;
          db.Persona.Add(persona);

          db.SaveChanges();

          var id_persona = persona.Id;

          var perfil = db.Perfil.First(u => u.Descripcion == "Administrador");
          var usuario = new Usuario();

          usuario.Email = mba.usuario.email;
          usuario.Password = auth.getHash(mba.usuario.password); // hasheo le password
          usuario.IdPerfil = perfil.Id;
          db.Usuario.Add(usuario);

          db.SaveChanges();

          var tipo_empleado = db.TipoEmpleado.First(te => te.Descripcion == "Administrador");
          var id_usuario = usuario.Id;
          var id_tipo_empleado = tipo_empleado.Id;

          var empleado = new Empleado();
          empleado.IdBarrio = mba.barrio.Id;
          empleado.IdPersona = id_persona;
          empleado.IdUsuario = id_usuario;
          empleado.IdTipoEmpleado = id_tipo_empleado;
          db.Empleado.Add(empleado);

          db.SaveChanges();

          transaction.Commit();
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", message = err.Message };
        }
      }

      return new { error = false, data = "ok" };
    }

    [HttpPut("{id}")]
    public Object Put(int id, [FromBody]Barrio barrio)
    {
      barrio.Id = id;
      db.Barrio.Update(barrio);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpDelete("{id}")]
    public Object Delete(int id)
    {
      if (db.Barrio.Where(t => t.Id == id).Count() > 0) // Check if element exists
        db.Barrio.Remove(db.Barrio.First(t => t.Id == id));

      db.SaveChanges();

      return new { error = false, data = "ok" };
    }
  }

}
