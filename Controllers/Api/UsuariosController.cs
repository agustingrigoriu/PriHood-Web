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
  [Route("api/[controller]")]
  public class UsuariosController : Controller
  {
    private readonly PrihoodContext db;
    private AuthService auth;
    public UsuariosController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
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
          var persona = new Persona();
          persona.Apellido = mres.apellido;
          persona.Nombre = mres.nombre;
          persona.FechaNacimiento = mres.fecha_nacimiento;
          persona.IdTipoDocumento = mres.id_tipoDocumento;
          persona.NroDocumento = mres.nroDocumento;
          persona.TelefonoMovil = mres.telefono;
          db.Persona.Add(persona);

          db.SaveChanges();

          var id_persona = persona.Id;

          var perfil = db.Perfil.First(u => u.Descripcion == "RESIDENTE");
          var usuario = new Usuario();

          usuario.Email = mres.email;
          usuario.Password = auth.getHash(mres.password); // hasheo le password
          usuario.IdPerfil = perfil.Id; //Tengo q buscar el correspondiente a Residente, no manejarme por ID
          db.Usuario.Add(usuario);

          db.SaveChanges();

          var id_usuario = usuario.Id;

          var residente = new Residente();
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

    [HttpPost("contraseña")]
    public Object CambiarContraseña([FromBody]ModeloEmail mod)
    {
      var usuario = db.Usuario.Where(u => u.Email.Equals(mod.email))
                                    .FirstOrDefault();

      if (usuario == null)
      return new { error=true, data="Usuario inexistente"};
      else {
        var randomPassword = RandomString(6);
        usuario.Password = getHash(randomPassword);
        db.Usuario.Update(usuario);
        //SendEmail("noreply@prihood.com","agustin.gregorieu@gmail.com","Cambio de Contraseña", randomPassword);
        db.SaveChanges();
        return new {error = true, data = "Contraseña generada con éxito"};
      }
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

    //Funciones
    public void SendEmail(string From, string To, string Subject, string Body){
      var email = Email
                  .From(From)
                  .To(To)
                  .Subject(Subject)
                  .Body(Body);

      email.Send();
    }
    public string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "abcdefghikjklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public string getHash(string text)
    {
      using (var sha256 = SHA256.Create())
      {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower().Substring(0, 30);
      }
    }
  }

  public class ModeloLogin
  {
    public string email { get; set; }
    public string password { get; set; }


  }
  public class ModeloEmail
  {
    public string email { get; set; }


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
