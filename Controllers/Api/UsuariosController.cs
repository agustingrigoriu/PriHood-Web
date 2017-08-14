using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class UsuariosController : Controller
  {
    private readonly PrihoodContext db;
    public UsuariosController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet]
    public Object Get()
    {
      return new { error = false, data = db.Usuario.ToList() };
    }

    [HttpGet("{id}")]
    public Object Get(int id)
    {
      return new { error = false, data = db.Usuario.First(u => u.Id == id) };
    }

    [HttpPost]
    public Object Post([FromBody]Usuario usuario)
    {
      db.Usuario.Add(usuario);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpPost("residente")]
    public Object AgregarUsuarioResidente([FromBody]ModeloResidente mres)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          Persona persona = new Persona();
          persona.Apellido = mres.apellido;
          persona.Nombre = mres.nombre;
          persona.FechaNacimiento = mres.fecha_nacimiento;
          persona.IdTipoDocumento = mres.id_tipoDocumento;
          persona.NroDocumento = mres.nroDocumento;
          persona.TelefonoMovil = mres.telefono;
          db.Persona.Add(persona);

          db.SaveChanges();

          var id_persona = persona.Id;

          Usuario usuario = new Usuario();
          usuario.Email = mres.email;
          usuario.Password = mres.password;
          Perfil perfil = db.Perfil.First(u => u.Descripcion == "RESIDENTE");
          usuario.IdPerfil = perfil.Id; //Tengo q buscar el correspondiente a Residente, no manejarme por ID
          db.Usuario.Add(usuario);

          db.SaveChanges();

          var id_usuario = usuario.Id;

          Residente residente = new Residente();
          residente.IdResidencia = mres.id_residencia;
          residente.IdPersona = id_persona;
          residente.IdUsuario = id_usuario;
          db.Residente.Add(residente);

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

    [HttpPut("{id}")]
    public Object Put(int id, [FromBody]Usuario usuario)
    {
      db.Usuario.Update(usuario);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpDelete("{id}")]
    public Object Delete(int id)
    {
      if (db.Usuario.Where(t => t.Id == id).Count() > 0) // Check if element exists
        db.Usuario.Remove(db.Usuario.First(t => t.Id == id));
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }
  }

  public class ModeloLogin
  {
    public string email { get; set; }
    public string password { get; set; }


  }

  public class ModeloResidente
  {
    public string nombre { get; set; }
    public string apellido { get; set; }
    public int id_tipoDocumento { get; set; }
    public string nroDocumento { get; set; }
    public string telefono { get; set; }
    public DateTime fecha_nacimiento { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int id_residencia { get; set; }

  }
}
