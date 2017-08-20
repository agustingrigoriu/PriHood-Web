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
    public Object RegistrarVisita([FromBody]ModeloVisitante mv)
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

        db.Visitante.Add(visitante);
        db.SaveChanges();

        return new { error = false, data = visitante };
      }
      catch (Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

    [HttpPost("{id}/{evento}")]
    public Object MarcarEvento(int id, string evento)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var evento_visita = db.EventoVisita.Where(e => e.Nombre == evento).First();

          var visita = new Visita();
          visita.Fecha = DateTime.Now;
          visita.IdEvento = evento_visita.Id;
          visita.IdVisitante = id;

          db.Visita.Add(visita);
          db.SaveChanges();

          if (evento == "ingreso")
          {
            var visitante = db.Visitante.First(v => v.Id == id);

            var visitaxresidente = new VisitasXresidente();
            visitaxresidente.IdVisita = visita.Id;
            visitaxresidente.IdResidente = visitante.IdResidente;
            db.VisitasXresidente.Add(visitaxresidente);
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
    public Object ListarVisitas()
    {
      try
      {
        var hoy = DateTime.Today;

        var visitas = (
          from v in db.Visita
          join vs in db.Visitante on v.IdVisitante equals vs.Id
          join tv in db.TipoVisita on vs.IdTipoVisita equals tv.Id
          join td in db.TipoDocumento on vs.IdTipoDocumento equals td.Id
          where v.Fecha.Date == hoy || tv.Nombre == "frecuente"
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
            estado = "-" // por ahora no se calcula.. pero debe decir si ingreso o egreso o no llego
          }
        ).ToList();

        return new { error = false, data = visitas };
      }
      catch (Exception)
      {
        return new { error = true, data = "fail" };
      }
    }
  }
}