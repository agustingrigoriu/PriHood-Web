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
  }

  public class LoginModel
  {
    public string email { get; set; }
    public string password { get; set; }
  }
}
