using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using PriHood.Auth;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class CarpoolingController : Controller
  {

    private readonly PrihoodContext db;
    private AuthService auth;

    private PushService push;
    public CarpoolingController(PrihoodContext context, AuthService a, PushService p)
    {
      db = context;
      auth = a;
      push = p;
    }


    [HttpPost]
    public Object RegistrarViaje([FromBody]ModeloCarpooling mc)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);

          mc.viaje.IdResidente = residente.Id;
          db.Viaje.Add(mc.viaje);
          db.SaveChanges();

          foreach (var trayecto in mc.trayectos)
          {
            trayecto.IdViaje = mc.viaje.Id;

            db.Trayecto.Add(trayecto);
          }
          db.SaveChanges();

          transaction.Commit();
          return new { error = false, data = mc.viaje };

        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", message = err.Message };
        }
      }
    }


    //Listo los viajes existentes en el barrio del usuario
    [HttpGet("viajes/{fecha:DateTime}")]
    public Object ListarViajes(DateTime fecha)
    {
      try
      {

        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        var viajes = (
          from v in db.Viaje
          join r in db.Residente on v.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join p in db.Persona on r.IdPersona equals p.Id
          where u.IdBarrio == id_barrio && (v.Fecha.HasValue ? v.Fecha.Value.Date == fecha.Date : true)
          select new
          {
            v.Id,
            v.AutoModelo,
            v.AutoColor,
            v.AutoAsientos,
            v.AutoPatente,
            v.Fecha,
            v.Observaciones,
            v.IdDiaSemana,
            v.SaleBarrio,
            v.IdResidente,
            tipo = v.Fecha.HasValue ? "único" : "recurrente",
            residente = p.Apellido + ", " + p.Nombre,
            trayectos = (
              from tr in db.Trayecto
              where tr.IdViaje == v.Id
              orderby tr.Orden ascending
              select tr
            )
          }
        );

        return new { error = false, data = viajes };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    //Recordar agregar atributo de DESTINO en Viajes para mostrar en las solicitudes
    [HttpGet("solicitudes")]
    public Object ListarMisSolicitudes()
    {
      try
      {

        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        var solicitudes = (
          from s in db.SolicitudViaje
          join v in db.Viaje on s.IdViaje equals v.Id
          join r in db.Residente on v.IdResidente equals r.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join b in db.Barrio on id_barrio equals b.Id
          join ds in db.DiaSemana on v.IdDiaSemana equals ds.Id
          join es in db.EstadoSolicitud on s.IdEstadoSolicitud equals es.Id
          where s.IdResidente == logueado.Id
          select new
          {
            u.Avatar,
            p.Nombre,
            p.Apellido,
            v.SaleBarrio,
            nombreBarrio = b.Nombre,
            v.IdDiaSemana,
            v.Fecha,
            v.Hora,
            v.AutoAsientos,
            v.AutoColor,
            v.AutoModelo,
            v.AutoPatente,
            v.Observaciones,
            es.Descripcion,
            v.Destino,
            trayectos = (
              from tr in db.Trayecto
              where tr.IdViaje == v.Id
              orderby tr.Orden ascending
              select tr
            )
          }
        );

        return new { error = false, data = solicitudes };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpGet("ofrecimientos")]
    public Object ListarMisOfrecimientos()
    {
      try
      {

        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        var ofrecimientos = (
          from s in db.SolicitudViaje
          join r in db.Residente on s.IdResidente equals r.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join v in db.Viaje on s.IdViaje equals v.Id
          join ds in db.DiaSemana on v.IdDiaSemana equals ds.Id
          join es in db.EstadoSolicitud on s.IdEstadoSolicitud equals es.Id
          where v.IdResidente == logueado.Id
          select new
          {
            u.Avatar,
            p.Nombre,
            p.Apellido,
            v.IdDiaSemana,
            v.Fecha,
            v.Hora,
            es.Descripcion
          }
        );

        return new { error = false, data = ofrecimientos };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpPost("solicitar_viaje/{id_viaje}")]
    public Object SolicitarViaje(int id_viaje)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
          var estado = db.EstadoSolicitud.First(e => e.Descripcion == "Pendiente");

          var datos = (
            from v in db.Viaje
            join r in db.Residente on v.IdResidente equals r.Id
            where v.Id == id_viaje
            select new
            {
              viaje = v,
              residente = r
            }
          ).First();

          if (datos.viaje.AutoAsientos <= 0)
          {
            throw new Exception("No hay asientos disponibles.");
          }

          var solicitud = new SolicitudViaje
          {
            Fecha = DateTime.Now,
            IdEstadoSolicitud = estado.Id,
            IdResidente = residente.Id,
            IdViaje = datos.viaje.Id
          };

          db.SolicitudViaje.Add(solicitud);
          db.SaveChanges();

          transaction.Commit();

          this.push.enviarMensaje(datos.residente.IdUsuario, "Un residente quiere unirse a tu viaje.");

          return new { error = false, data = solicitud };
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", message = err.Message };
        }
      }
    }

    [HttpPost("ofrecimientos/{id_solicitud_viaje}")]
    public Object AceptarRechazarSolicitud(int id_solicitud_viaje, [FromBody] ModeloEstadoSolicitud es)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
          var estado = db.EstadoSolicitud.First(e => e.Descripcion == es.estado_solicitud);
          var solicitud = db.SolicitudViaje.First(s => s.Id == id_solicitud_viaje);

          solicitud.IdEstadoSolicitud = estado.Id;

          db.SolicitudViaje.Update(solicitud);
          db.SaveChanges();

          transaction.Commit();
          return new { error = false, data = es };
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", message = err.Message };
        }
      }

    }

  }
}
