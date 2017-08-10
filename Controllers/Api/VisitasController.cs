using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PriHood.Controllers
{
  public class ApiVisitasController : Controller
  {

    [HttpGet]
    [Route("/api/visitas")]
    public IActionResult getVisitas()
    {
      var json = new { mensaje = "por get!" };
      return Json(json);
    }

    [HttpPost]
    
    [Route("/api/visitas")]
    public IActionResult postVisitas([FromBody]Dictionary<String, String> visita)
    {
      var json = new { mensaje = "por post!", visita = visita };

      return Json(json);
    }

  }


  public class FormVisita {
    public string nombre {get;set;}
    public int numero {get;set;}
  }
}