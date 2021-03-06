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

        var cantidad_amenities = (
          from a in db.Amenity
          where a.IdBarrio == id_barrio
          select a
        ).Count();

        var visitasDataBar = (
          from v in db.Visita
          join vi in db.Visitante on v.IdVisitante equals vi.Id
          join r in db.Residente on vi.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join tv in db.TipoVisita on vi.IdTipoVisita equals tv.Id
          join ee in db.EventoVisita on v.IdEvento equals ee.Id
          where v.Fecha.Date > DateTime.Now.Date.AddDays(-10) && u.IdBarrio == id_barrio && ee.Nombre == "Ingreso"
          orderby v.Fecha.Date descending
          group v by v.Fecha.Date into c
          select new
          {
            label = c.Key.Date.ToString("dd/MM/yyyy"),
            count = c.Count()
          }
          );

        // var visitasActualDataBar = (
        //   from v in db.Visita
        //   join vi in db.Visitante on v.IdVisitante equals vi.Id
        //   join r in db.Residente on vi.IdResidente equals r.Id
        //   join u in db.Usuario on r.IdUsuario equals u.Id
        //   join tv in db.TipoVisita on vi.IdTipoVisita equals tv.Id
        //   join ee in db.EventoVisita on v.IdEvento equals ee.Id
        //   where v.Fecha > DateTime.Now.AddDays(-10) && u.IdBarrio == id_barrio && tv.Nombre == "Actual" && ee.Nombre == "Ingreso"
        //   orderby v.Fecha descending
        //   group v by v.Fecha.Date into c
        //   select new
        //   {
        //     label = c.Key.Date.ToString("dd/MM/yyyy"),
        //     count = c.Count()
        //   }
        //   );

        var recaudacionReservasLine = (
        from r in db.Reserva
        join t in db.Turno on r.IdTurno equals t.Id
        join a in db.Amenity on t.IdAmenity equals a.Id
        where a.IdBarrio == id_barrio && r.Fecha.Year == DateTime.Now.Year
        group r.Costo by r.Fecha.Month into c
        select new
        {
          label = c.Key,
          sum = c.Sum()
        }
        );


        var amenitiesDataPie = (
          from r in db.Reserva
          join t in db.Turno on r.IdTurno equals t.Id
          join a in db.Amenity on t.IdAmenity equals a.Id
          join ta in db.TipoAmenity on a.IdTipoAmenity equals ta.Id
          where a.IdBarrio == id_barrio && r.Fecha > DateTime.Now.AddDays(-30)
          group r by ta.Descripcion into c
          select new
          {
            label = c.Key,
            count = c.Count()
          }
        );


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
          visitasDataBar = visitasDataBar,
          amenitiesDataPie = amenitiesDataPie,
          cantidad_amenities = cantidad_amenities,
          recaudacionReservasLine = recaudacionReservasLine
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
