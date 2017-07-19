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
      return View();
    }

    public IActionResult Comunicaciones()
    {
      return View();
    }

    public IActionResult VerComunicacion(string index)
    {
      return View();
    }

    [HttpGet]
    public IActionResult Usuarios(string index, int rowId)
    {
      string[] Residencias = { "RES 1", "RES 2" };
      ViewData["Residencias"] = Residencias;
      string[] TiposDocumento = { "DNI", "PASAPORTE" };
      ViewData["TiposDocumento"] = TiposDocumento;
      string[] Perfiles = { "Administrador", "Residente", "Encagado de Seguridad" };
      ViewData["Perfiles"] = Perfiles;
      int idSelected = rowId;
      return View();
    }
    [HttpGet]
    public IActionResult Barrios(string index, int rowId)
    {
      string[] Residencias = { "RES 1", "RES 2" };
      ViewData["Residencias"] = Residencias;
      string[] TiposDocumento = { "DNI", "PASAPORTE" };
      ViewData["TiposDocumento"] = TiposDocumento;
      string[] Perfiles = { "Administrador", "Residente", "Encagado de Seguridad" };
      ViewData["Perfiles"] = Perfiles;
      int idSelected=rowId;
      return View();

      return View();


    }

    public IActionResult VerUsuarios(string index)
    {
      return View();
    }

    public IActionResult Residencias(string index)
    {
      ViewData["mundo"] = "probando!";
      return View();
    }


    public IActionResult Visitas()
    {

      return View();
    }

    
    [Route("/ejemplosPepe")]
    [Route("/Panel/ejemploPepe2")]
    public IActionResult Ejemplos()
    {
      ViewData["mundo"] = "probando!";
      ViewData["personas"] = new List<Persona>() {
            new Persona{nombre = "Pato perez", email = "pato@pato.com"},
            new Persona{nombre = "Pato2 perez", email = "pato2@pato.com"},
            new Persona{nombre = "Pato3 perez", email = "pato3@pato.com"},
            new Persona{nombre = "Pato4 perez", email = "pato4@pato.com"},
            new Persona{nombre = "Pato5 perez", email = "pato5@pato.com"}
        };

      return View();
    }
  }

  public class Persona
  {
    public string nombre { get; set; }
    public string email { get; set; }
  }
}
