using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PriHood.Controllers
{
  public class PanelController : Controller
  {
    public IActionResult Index()
    {

      // aca hay que hacer..
      // si esta logueado... muestro la vista, sino
      // redirijo al login
      
      return View();
    }
  }
}
