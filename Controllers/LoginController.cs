using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Auth;
using PriHood.Models;

namespace PriHood.Controllers
{
  public class LoginController : Controller
  {
    private AuthService _AuthService;
    private PrihoodContext _db;
    public LoginController(PrihoodContext db, AuthService AuthService)
    {
      this._AuthService = AuthService;
      this._db = db;
    }

    public IActionResult Index()
    {
      if (HttpContext.Session.Authenticated() == null)
        return View();

      return Redirect("/panel");
    }

    [HttpPost]
    public Object Index(LoginModel login)
    {
      var usuario = _AuthService.Login(login.email ?? "", login.password ?? "");

      if (usuario.IdPerfil == 3)
      {
        ViewData["error"] = "Los residentes no pueden acceder";
        return View();
      }

      if (usuario != null)
      {
        HttpContext.Session.LogInAccount(usuario);
        return Redirect("/panel");
      }

      ViewData["error"] = "Email o contraseña incorrectos.";
      return View();
    }

    [Route("/Logout")]
    public IActionResult Logout()
    {
      if (HttpContext.Session.Authenticated() == null)
        return Redirect("/panel");

      HttpContext.Session.LogOutAccount();

      return Redirect("/");
    }

    [HttpGet("/api/login")]
    public Object ApiLogin()
    {
      var usuario = HttpContext.Session.Authenticated();

      if (usuario == null) return new { error = true, data = "" };
      if (usuario.IdBarrio != null)
      {
        var datos_empleado = (
          from u in _db.Usuario
          join e in _db.Empleado on u.Id equals e.IdUsuario
          join p in _db.Persona on e.IdPersona equals p.Id
          join per in _db.Perfil on u.IdPerfil equals per.Id
          join b in _db.Barrio on u.IdBarrio equals b.Id
          where u.Id == usuario.Id
          select new
          {
            nombre = p.Nombre,
            apellido = p.Apellido,
            idPerfil = u.IdPerfil,
            email = u.Email,
            avatar = u.Avatar,
            perfil = per.Descripcion,
            barrio = b.Nombre
          }
        ).First();

        return new { error = usuario == null, data = datos_empleado };
      }
      else
      {
        var datos_root = (
          from u in _db.Usuario
          join per in _db.Perfil on u.IdPerfil equals per.Id
          select new
          {
            nombre = "ROOT",
            apellido = "",
            idPerfil = u.IdPerfil,
            email = u.Email,
            avatar = u.Avatar,
            perfil = per.Descripcion,
            barrio = "Súper Administrador"
          }
        ).First();

        return new { error = usuario == null, data = datos_root };
      }

    }

    [HttpPost("/api/token")]
    public Object ApiToken([FromBody]LoginModel login)
    {
      var usuario = _AuthService.Login(login.email ?? "", login.password ?? "");

      if (usuario == null)
      {
        return StatusCode(401, new { error = true, data = null as Usuario });
      }

      var token = _AuthService.getToken(usuario);

      return new { error = false, data = new { token = token, usuario = usuario } };
    }

  }

  public class LoginModel
  {
    public string email { get; set; }
    public string password { get; set; }
  }
}
