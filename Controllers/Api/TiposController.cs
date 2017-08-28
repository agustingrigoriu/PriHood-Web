using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using PriHood.Auth;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class TiposController : Controller
  {

    private readonly PrihoodContext db;
    public TiposController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet("documento")]
    public Object GetTipoDocumento()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var tiposDocumento = db.TipoDocumento.ToList();

        return new { error = false, data = tiposDocumento };
      }
      catch (System.Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

    [HttpGet("empleado")]
    public Object GetTipoEmpleado()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var tiposEmpleado = db.TipoEmpleado.ToList();

        return new { error = false, data = tiposEmpleado };
      }
      catch (System.Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

    [HttpGet("servicio")]
    public Object GetTipoServicio()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var tiposServicios = db.TipoServicio.ToList();

        return new { error = false, data = tiposServicios };
      }
      catch (System.Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

    [HttpGet("visita")]
    public Object GetTipoVisita()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var tiposVisita = db.TipoVisita.ToList();

        return new { error = false, data = tiposVisita };
      }
      catch (System.Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

  }
}
