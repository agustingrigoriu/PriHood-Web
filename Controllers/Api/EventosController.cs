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
  public class EventosController : Controller
  {
    private readonly PrihoodContext db;
    public EventosController(PrihoodContext context)
    {
      db = context;
    }

    [HttpPost]
    public Object CrearEvento([FromBody]Eventos evento)
    {
      try
      {

        db.Eventos.Add(evento);
        db.SaveChanges();

        return new { error = false, data = evento };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpPut("{id_evento}")]
    public Object ActualizarEvento(int id_evento, [FromBody]ModeloEventosUpdate eventoNuevo)
    {
      try
      {
        var evento = db.Eventos.First(e => e.Id == id_evento);

        evento.Descripcion = eventoNuevo.Descripcion != null ? eventoNuevo.Descripcion : evento.Descripcion;
        evento.Imagen = eventoNuevo.Imagen != null ? eventoNuevo.Imagen : evento.Imagen;
        evento.Duracion = eventoNuevo.Duracion.HasValue ? eventoNuevo.Duracion.Value : evento.Duracion;
        evento.HoraDesde = eventoNuevo.HoraDesde.HasValue ? eventoNuevo.HoraDesde.Value : evento.HoraDesde;
        evento.Fecha = eventoNuevo.Fecha.HasValue ? eventoNuevo.Fecha.Value : evento.Fecha;
        evento.IdResidente = eventoNuevo.IdResidente.HasValue ? eventoNuevo.IdResidente.Value : evento.IdResidente;
        evento.IdTipoEvento = eventoNuevo.IdTipoEvento.HasValue ? eventoNuevo.IdTipoEvento.Value : evento.IdTipoEvento;

        db.Eventos.Update(evento);
        db.SaveChanges();

        return new { error = false, data = "ok" };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpDelete("{id_evento}")]
    public Object BorrarEvento(int id_evento)
    {
      try
      {
        var evento = db.Eventos.First(e => e.Id == id_evento);

        db.Eventos.Remove(evento);
        db.SaveChanges();

        return new { error = false, data = "ok" };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }


    [HttpGet("{fecha:DateTime}")]
    public Object ListarEventosPorFecha(DateTime fecha)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var eventos = (
          from e in db.Eventos
          join r in db.Residente on e.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join te in db.TipoEvento on e.IdTipoEvento equals te.Id
          where u.IdBarrio == id_barrio && e.Fecha.Date == fecha.Date
          select new
          {
            id_evento = e.Id,
            descripcion = e.Descripcion,
            duracion = e.Duracion,
            fecha = e.Fecha,
            hora_desde = e.HoraDesde,
            imagen = e.Imagen,
            id_tipo_evento = e.IdTipoEvento,
            tipo_evento = te.Descripcion,
            id_residente = e.IdResidente,
            residente = p.Apellido + ", " + p.Nombre
          }
        ).ToList();

        return new { error = false, data = eventos };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("asistencias/{id_evento}")]
    public Object ListarAsistenciasEvento(int id_evento)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var asistencias = (
          from a in db.AsistenciaEvento
          join r in db.Residente on a.IdResidente equals r.Id
          join p in db.Persona on r.IdPersona equals p.Id
          where a.IdEvento == id_evento
          select new
          {
            id_evento = a.IdEvento,
            id_residente = r.Id,
            residente = p.Apellido + ", " + p.Nombre
          }
        ).ToList();

        return new { error = false, data = asistencias };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpPost("asistencias/{id_evento}")]
    public Object ConfirmarAsistencia(int id_evento)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var residente = (
          from r in db.Residente
          join u in db.Usuario on r.IdUsuario equals u.Id
          where u.Id == logueado.Id
          select r
        ).First();

        AsistenciaEvento asistencia = new AsistenciaEvento();
        asistencia.IdEvento = id_evento;
        asistencia.IdResidente = residente.Id;
        db.AsistenciaEvento.Add(asistencia);
        db.SaveChanges();

        return new { error = false, data = asistencia };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpDelete("asistencias/{id_evento}")]
    public Object EliminarAsistencia(int id_evento, int id_residente)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var residente = (
          from r in db.Residente
          join u in db.Usuario on r.IdUsuario equals u.Id
          where u.Id == logueado.Id
          select r
        ).First();

        AsistenciaEvento asistencia = new AsistenciaEvento();
        asistencia.IdEvento = id_evento;
        asistencia.IdResidente = residente.Id;
        db.AsistenciaEvento.Remove(asistencia);
        db.SaveChanges();

        return new { error = false, data = asistencia };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

  }
}
