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
    private PushService pushService;
    public EventosController(PrihoodContext context, PushService ps)
    {
      db = context;
      pushService = ps;
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

        evento.Titulo = eventoNuevo.Titulo != null ? eventoNuevo.Titulo : evento.Titulo;
        evento.Descripcion = eventoNuevo.Descripcion != null ? eventoNuevo.Descripcion : evento.Descripcion;
        evento.HoraHasta = eventoNuevo.HoraHasta.HasValue ? eventoNuevo.HoraHasta.Value : evento.HoraHasta;
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


    [HttpGet]
    public Object ListarProximosEventos()
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
          where u.IdBarrio == id_barrio && e.Fecha.Date >= DateTime.Now.Date
          select new
          {
            id_evento = e.Id,
            descripcion = e.Descripcion,
            titulo = e.Titulo,
            hora_hasta = e.HoraHasta,
            fecha = e.Fecha,
            hora_desde = e.HoraDesde,
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

    [HttpGet("comentarios/{id_evento}")]
    public Object ListarComentarios(int id_evento)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var comentarios = (
          from c in db.ComentariosEvento
          join e in db.Eventos on c.IdEvento equals e.Id
          join r in db.Residente on e.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          where u.IdBarrio == id_barrio
          orderby c.Fecha ascending
          select new
          {
            id = c.Id,
            comentario = c.Texto,
            fecha = c.Fecha,
            usuario = u.Email
          }
        )
        .ToList();

        return new { error = false, data = comentarios };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpPost("{id_evento}/comentar")]
    public Object RegistrarComentario(int id_evento, [FromBody]ComentariosEvento comentario)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var evento = db.Eventos.First(e => e.Id == id_evento);

          comentario.IdEvento = evento.Id;
          comentario.Fecha = DateTime.Now;
          comentario.IdUsuario = logueado.Id;
          db.ComentariosEvento.Add(comentario);

          db.SaveChanges();


          //obtengo id de usuario del residente que creó el evento y le notifico del comentario
          var id_usuario_residente = ( 
            from u in db.Usuario 
            join r in db.Residente on u.Id equals r.IdUsuario
            where r.Id == evento.IdResidente
            select u.Id
          ).First();

          this.pushService.enviarMensaje(id_usuario_residente, "Se ha realizado un comentario en su evento \"" + evento.IdTipoEvento + "\".");

          transaction.Commit();
        }
        catch (Exception)
        {
          transaction.Rollback();
          return new { error = true, data = "Error" };
        }
      }

      return new { error = false, data = "ok" };
    }

  }
}
