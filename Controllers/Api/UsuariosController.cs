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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class UsuariosController : Controller
  {
    private readonly PrihoodContext db;
    private AuthService auth;
    private readonly UploadService uploadService;

    private EmailService email;
    private IHostingEnvironment _hostingEnvironment;
    public UsuariosController(PrihoodContext context, AuthService a, EmailService e, IHostingEnvironment environment, UploadService ul)
    {
      db = context;
      auth = a;
      email = e;
      _hostingEnvironment = environment;
      uploadService = ul;
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
          persona.IdTipoDocumento = mres.id_tipo_documento;
          persona.NroDocumento = mres.numero_documento;
          persona.TelefonoMovil = mres.telefono;
          db.Persona.Add(persona);

          db.SaveChanges();

          var id_persona = persona.Id;

          var residencia = db.Residencia.Where(r => r.Id == mres.id_residencia).First();
          var perfil = db.Perfil.First(u => u.Descripcion == "RESIDENTE");
          var usuario = new Usuario();

          usuario.Email = mres.email;
          usuario.Password = auth.getHash(mres.password); // hasheo le password
          usuario.IdPerfil = perfil.Id; //Tengo q buscar el correspondiente a Residente, no manejarme por ID
          usuario.IdBarrio = residencia.IdBarrio;
          usuario.Avatar = mres.avatar;
          
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

    [HttpPost("avatar")]
    public Object AgregarAvatar(IFormFile avatar)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var usuario = (
            from u in db.Usuario
            where u.Id == logueado.Id
            select u
          ).FirstOrDefault();

          if (usuario == null) return new { error = true, data = "Error" };

          var url_avatar = this.uploadService.UploadFile(avatar);
          usuario.Avatar = url_avatar;
          db.Usuario.Update(usuario);

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

    [HttpPost("push/token")]
    public Object RegistrarTokenPush([FromBody]ModeloToken mt)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          logueado.Token = mt.token;

          db.Usuario.Update(logueado);
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

    [HttpPost("password")]
    public Object ResetPassword([FromBody]ModeloPassword mc)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var nuevoPassword = RandomString(6);
          var usuario = db.Usuario.FirstOrDefault(u => u.Email == mc.email);
          if (usuario == null) return new { error = true, data = "No existe usuario registrado con ese email" };

          usuario.Password = auth.getHash(nuevoPassword);
          db.Usuario.Update(usuario);
          db.SaveChanges();
          email.SendEmailPwdChanged(usuario.Email, nuevoPassword);
          transaction.Commit();
        }
        catch (Exception e)
        {
          transaction.Rollback();
          return new { error = true, data = e.Message };
        }
      }

      return new { error = false, data = "ok" };
    }

    [HttpPost("changepassword")]
    public Object ChangePassword([FromBody]ModeloChangePassword mc)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();

          var usuario = db.Usuario.FirstOrDefault(u => u.Id == logueado.Id);

          if (usuario == null) return new { error = true, data = "Usuario inexistente" };

          if (usuario.Password != auth.getHash(mc.password_actual)) return new { error = true, data = "Contraseña incorrecta" };

          usuario.Password = auth.getHash(mc.password_nueva);
          db.Usuario.Update(usuario);
          db.SaveChanges();

          transaction.Commit();
        }
        catch (Exception e)
        {
          transaction.Rollback();
          return new { error = true, data = e.Message };
        }
      }

      return new { error = false, data = "ok" };
    }
    public string RandomString(int length)
    {
      Random random = new Random();
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnñopqrstuvwxyz";
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
  }
}
