using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using PriHood.Auth;
using System.Security.Cryptography;
using System.Text;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class DashboardController : Controller
  {
    private readonly PrihoodContext db;
    public DashboardController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet("root")]
    public Object DashboardRoot(int? id_empleado)
    {
      try
      {

        var logueado = HttpContext.Session.Authenticated();
        var cantidad_barrios = (
          from b in db.Barrio
          select b
        ).Count();

        var cantidad_residentes = (
          from r in db.Residente
          select r
        ).Count();

        var data = new { cantidad_barrios = cantidad_barrios, cantidad_residentes = cantidad_residentes };

        return new { error = false, data = data };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("administrador")]
    public Object DashboardAdministrador()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio;
        var cantidad_residentes = (
          from r in db.Residente
          join u in db.Usuario on r.IdUsuario equals u.Id
          where u.IdBarrio == id_barrio
          select r
        ).Count();

        var cantidad_residencias = (
          from r in db.Residencia
          where r.IdBarrio == id_barrio
          select r
        ).Count();

        var data = new { cantidad_residencias = cantidad_residencias, cantidad_residentes = cantidad_residentes };

        return new { error = false, data = data };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

  }
}
