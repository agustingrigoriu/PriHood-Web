using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Auth;
using PriHood.Models;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class VisitasController : Controller
  {

    private readonly PrihoodContext db;
    private AuthService auth;
    public VisitasController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
    }

    [HttpPost]
    public Object RegistrarVisitante([FromBody]ModeloVisitante mv)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
          var visitante = new Visitante();

          visitante.Apellido = mv.apellido;
          visitante.Avatar = mv.avatar;
          visitante.FechaVisita = mv.fecha_visita;
          visitante.IdTipoDocumento = mv.id_tipo_documento;
          visitante.IdTipoVisita = mv.id_tipo_visita;
          visitante.Nombre = mv.nombre;
          visitante.NumeroDocumento = mv.numero_documento;
          visitante.Observaciones = mv.observaciones;
          visitante.Patente = mv.patente;
          visitante.IdResidente = residente.Id;
          visitante.Estado = "esperando";

          db.Visitante.Add(visitante);
          db.SaveChanges();

          transaction.Commit();

          return new { error = false, data = visitante };
        }
        catch (Exception)
        {
          transaction.Rollback();

          return new { error = true, data = "fail" };
        }
      }
    }

    [HttpPost("marcar/{id}/{evento}")]
    public Object MarcarEvento(int id, string evento)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var evento_visita = db.EventoVisita.Where(e => e.Nombre == evento).First();
          var visitante = db.Visitante.First(v => v.Id == id);

          var visita = new Visita();
          visita.Fecha = DateTime.Now;
          visita.IdEvento = evento_visita.Id;
          visita.IdVisitante = id;

          db.Visita.Add(visita);
          db.SaveChanges();

          if (evento == "ingreso")
          {

            var visitaxresidente = new VisitasXresidente();
            visitaxresidente.IdVisita = visita.Id;
            visitaxresidente.IdResidente = visitante.IdResidente;
            
            db.VisitasXresidente.Add(visitaxresidente);
            
            visitante.Estado = "ingreso";

            db.SaveChanges();
          } else {
            visitante.Estado = "egreso";

            db.SaveChanges();
          }

          transaction.Commit();

          return new { error = false, data = visita };
        }
        catch (Exception)
        {
          transaction.Rollback();

          return new { error = true, data = "fail" };
        }
      }
    }


    [HttpGet]
    public Object ListarVisitantes()
    {
      try
      {
        var hoy = DateTime.Today;
        var logueado = HttpContext.Session.Authenticated();

        var visitas = (
          from vs in db.Visitante

          join tv in db.TipoVisita on vs.IdTipoVisita equals tv.Id
          join td in db.TipoDocumento on vs.IdTipoDocumento equals td.Id
          join rs in db.Residente on vs.IdResidente equals rs.Id
          join us in db.Usuario on rs.IdUsuario equals us.Id

          where ((vs.FechaVisita.HasValue && vs.FechaVisita.Value.Date == hoy) || tv.Nombre == "Frecuente") && us.IdBarrio == logueado.IdBarrio

          select new
          {
            apellido = vs.Apellido,
            nombre = vs.Nombre,
            id = vs.Id,
            numero_documento = vs.NumeroDocumento,
            observaciones = vs.Observaciones,
            patente = vs.Patente,
            tipo_visita = tv.Nombre,
            tipo_documento = td.Descripcion,
            estado = vs.Estado
          }
        ).ToList();

        return new { error = false, data = visitas, logueado  };
      }
      catch (Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

    [HttpGet("tipo-visita/{id}")]
    public Object ListarVisitantesPorTipo(int id)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);

        var visitas = (
          from vs in db.Visitante
          join tv in db.TipoVisita on vs.IdTipoVisita equals tv.Id
          join td in db.TipoDocumento on vs.IdTipoDocumento equals td.Id
          where (vs.Estado == "esperando" || tv.Nombre == "frecuente") && vs.IdTipoVisita == id && vs.IdResidente == residente.Id
          select new
          {
            apellido = vs.Apellido,
            nombre = vs.Nombre,
            id = vs.Id,
            numero_documento = vs.NumeroDocumento,
            observaciones = vs.Observaciones,
            patente = vs.Patente,
            id_tipo_documento = vs.IdTipoDocumento,
            avatar = vs.Avatar,
            fecha_visita = vs.FechaVisita
          }
        ).ToList();

        return new { error = false, data = visitas };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", err.Message, asd = err.Data, err.InnerException, err.Source };
      }
    }
  }
}