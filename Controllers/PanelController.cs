using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Auth;

namespace PriHood.Controllers
{
  public class PanelController : Controller
  {
    public IActionResult Index()
    {
      // si esta logueado... muestro la vista, sino
      // redirijo al login
      
      //if (HttpContext.Session.Authenticated() == null)
       // return Redirect("/Login");

      return View();
    }
  }
}
