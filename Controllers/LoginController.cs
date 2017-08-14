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

    public LoginController(AuthService AuthService)
    {
      this._AuthService = AuthService;
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

      return new { error = usuario == null, data = usuario };
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

      return new { error = false, data = new { token = token } };
    }

  }

  public class LoginModel
  {
    public string email { get; set; }
    public string password { get; set; }
  }
}
