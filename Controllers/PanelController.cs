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
      var idSelected=rowId;
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
  }
}
