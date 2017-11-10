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

        var visitasFrecuentesDataBar = (
          from v in db.Visita
          join vi in db.Visitante on v.IdVisitante equals vi.Id
          join r in db.Residente on vi.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join tv in db.TipoVisita on vi.IdTipoVisita equals tv.Id
          where v.Fecha > DateTime.Now.AddDays(-10) && u.IdBarrio == id_barrio && tv.Nombre == "Frecuente"
          orderby v.Fecha descending
          group v.Fecha by v.Id into c
          select new
          {
            labels = c.Select(key => new[] { c.Key }).ToArray(),
            count = c.Select(count => new[] { c.Count() }).ToArray()
          }
          ).ToArray();

        var visitasActualDataBar = (
          from v in db.Visita
          join vi in db.Visitante on v.IdVisitante equals vi.Id
          join r in db.Residente on vi.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join tv in db.TipoVisita on vi.IdTipoVisita equals tv.Id
          where v.Fecha > DateTime.Now.AddDays(-10) && u.IdBarrio == id_barrio && tv.Nombre == "Actual"
          orderby v.Fecha descending
          group v.Fecha by v.Id into c
          select new
          {
            labels = c.Select(key => new[] { c.Key }).ToArray(),
            count = c.Select(count => new[] { c.Count() }).ToArray()
          }
          ).First();


        var amenitiesDataPie = (
          from r in db.Reserva
          join t in db.Turno on r.IdTurno equals t.Id
          join a in db.Amenity on t.IdAmenity equals a.Id
          join ta in db.TipoAmenity on a.IdTipoAmenity equals ta.Id
          where a.IdBarrio == id_barrio && r.Fecha > DateTime.Now.AddDays(-365)
          group ta.Descripcion by r.Id into c
          select new
          {
            labels = c.Select(key => new[] { c.Key }).ToArray(),
            count = c.Select(count => new[] { c.Count() }).ToArray()
          }
        ).First();


        Barrio b = (
         from ba in db.Barrio
         where ba.Id == id_barrio
         select ba
       ).First();

        var data = new
        {
          cantidad_residencias = cantidad_residencias,
          cantidad_residentes = cantidad_residentes,
          latitud = b.Latitud,
          longitud = b.Longitud,
          visitasFrecuentesDataBar = visitasFrecuentesDataBar,
          visitasActualDataBar = visitasActualDataBar,
          amenitiesDataPie = amenitiesDataPie
        };

        return new { error = false, data = data };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

  }
}
