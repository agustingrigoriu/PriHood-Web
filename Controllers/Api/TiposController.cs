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

    [HttpGet("perfil")]
    public Object GetTipoEmpleado()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var tiposPerfil = db.Perfil.ToList();

        return new { error = false, data = tiposPerfil };
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

    [HttpGet("amenities")]
    public Object GetTiposAmenities()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var tipos = (
          from a in db.Amenity
          join ta in db.TipoAmenity on a.IdTipoAmenity equals ta.Id
          where a.IdBarrio == id_barrio
          select ta
        ).Distinct().ToList();

        return new { error = false, data = tipos };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("dias")]
    public Object GetDiasSemana()
    {
      try
      {
        var dias = (
          from d in db.DiaSemana
          select d
        ).ToList();

        return new { error = false, data = dias };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

  }
}
