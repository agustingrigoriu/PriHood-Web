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
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class TurnosController : Controller
  {
    private readonly PrihoodContext db;
    public TurnosController(PrihoodContext context)
    {
      db = context;
    }

    [HttpPost("{id_amenity}")]
    public Object CrearTurno([FromBody]Turno turno, int id_amenity)
    {
      try
      {
        turno.IdAmenity = id_amenity;

        db.Turno.Add(turno);
        db.SaveChanges();

        return new { error = false, data = turno };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpGet("{id_amenity}")]
    public Object ListarTurnos(int id_amenity)
    {
      try
      {
        var turnos = (
          from d in db.Turno
          where d.IdAmenity == id_amenity
          select d
        ).ToList();

        return new { error = false, data = turnos };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpPut("{id_turno}")]
    public Object ActualizarTurno(int id_turno, [FromBody]ModeloTurnoUpdate nuevoTurno)
    {
      try
      {
        var turno = db.Turno.First(t => t.Id == id_turno);

        turno.Costo = nuevoTurno.Costo.HasValue ? nuevoTurno.Costo.Value : turno.Costo;
        turno.Duracion = nuevoTurno.Duracion.HasValue ? nuevoTurno.Duracion.Value : turno.Duracion;
        turno.HoraDesde = nuevoTurno.HoraDesde.HasValue ? nuevoTurno.HoraDesde.Value : turno.HoraDesde;
        turno.IdDiaSemana = nuevoTurno.IdDiaSemana.HasValue ? nuevoTurno.IdDiaSemana.Value : turno.IdDiaSemana;
        turno.Nombre = nuevoTurno.Nombre != null ? nuevoTurno.Nombre : turno.Nombre;

        db.Turno.Update(turno);
        db.SaveChanges();

        return new { error = false, data = "ok" };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpDelete("{id_turno}")]
    public Object BorrarTurno(int id_turno)
    {
      try
      {
        var turno = db.Turno.First(t => t.Id == id_turno);

        db.Turno.Remove(turno);
        db.SaveChanges();

        return new { error = false, data = "ok" };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpGet("{id_amenity}/{fecha:DateTime}")]
    public Object ListarAmenitiesFechaTurnos(int id_amenity, DateTime fecha)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var id_dia = this.fecha2IdDay(fecha);
        var amenities = (
          from a in db.Amenity
          where a.IdBarrio == id_barrio && a.Id == id_amenity
          select new
          {
            id = a.Id,
            nombre = a.Nombre,
            descripcion = a.Descripcion,
            telefono = a.Telefono,
            ubicacion = a.Ubicacion,
            turnos = (
              from t in db.Turno
              where t.IdDiaSemana == id_dia && t.IdAmenity == a.Id
              select t
            )
          }
        ).First();

        return new { error = false, data = amenities };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    private int fecha2IdDay(DateTime fecha)
    {
      switch (fecha.DayOfWeek)
      {
        case DayOfWeek.Monday:
          return 1;
        case DayOfWeek.Tuesday:
          return 2;
        case DayOfWeek.Wednesday:
          return 3;
        case DayOfWeek.Thursday:
          return 4;
        case DayOfWeek.Friday:
          return 5;
        case DayOfWeek.Saturday:
          return 6;
        case DayOfWeek.Sunday:
        default:
          return 7;
      }
    }
  }
}
