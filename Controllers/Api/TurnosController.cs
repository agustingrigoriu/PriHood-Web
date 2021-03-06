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

    [HttpGet("{id_amenity:int}")]
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
    public Object ListarAmenityFechaTurnos(int id_amenity, DateTime fecha)
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
              select new
              {
                t.Costo,
                t.Duracion,
                t.HoraDesde,
                t.Id,
                t.IdAmenity,
                t.IdDiaSemana,
                t.Nombre,
                reservado = (
                  from r in db.Reserva
                  join er in db.EstadoReserva on r.IdEstadoReserva equals er.Id
                  where r.Fecha.Date == fecha.Date && r.IdTurno == t.Id && er.Descripcion == "creada"
                  select r
                ).Count() > 0
              }
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

    [HttpPost("{id_turno}/reservar")]
    public Object CrearReservaTurno(int id_turno, [FromBody]Reserva reserva)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
        var estado = db.EstadoReserva.First(e => e.Descripcion == "creada");
        var turno = (
          from t in db.Turno
          join a in db.Amenity on t.IdAmenity equals a.Id
          where t.Id == id_turno && a.IdBarrio == logueado.IdBarrio
          select t
        ).First();

        reserva.IdResidente = residente.Id;
        reserva.IdTurno = id_turno;
        reserva.Costo = turno.Costo;
        reserva.IdEstadoReserva = estado.Id;

        db.Reserva.Add(reserva);
        db.SaveChanges();

        return new { error = false, data = reserva };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpGet("reservas")]
    public Object ListarReservasResidente()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
        var data = (
          from r in db.Reserva
          join t in db.Turno on r.IdTurno equals t.Id
          join er in db.EstadoReserva on r.IdEstadoReserva equals er.Id
          join a in db.Amenity on t.IdAmenity equals a.Id
          where r.IdResidente == residente.Id && r.Fecha.Date >= DateTime.Now.Date
          orderby r.Fecha descending
          select new
          {
            reserva = new
            {
              r.Costo,
              r.Fecha,
              r.Id,
              estado = er.Descripcion
            },
            amenity = a,
            turno = t
          }
        ).OrderBy(r => r.reserva.Fecha);

        return new { error = false, data };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpPut("reservas/{id_reserva}/cancelar")]
    public Object CancelarReserva(int id_reserva)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
        var reserva = db.Reserva.First(t => t.Id == id_reserva);
        var estado = db.EstadoReserva.First(e => e.Descripcion == "cancelada");

        if (reserva.IdResidente != residente.Id)
        {
          throw new Exception("El usuario no corresponde con la reserva a cancelar");
        }
        reserva.IdEstadoReserva = estado.Id;

        db.Reserva.Update(reserva);
        db.SaveChanges();

        return new { error = false, data = "ok" };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
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
