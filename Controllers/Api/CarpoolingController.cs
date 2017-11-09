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

          //Controlo que la fecha del viaje sea mayor o igual a la del día de hoy, de no ser así se devuelve error.
          if (mc.viaje.Fecha < DateTime.Now) return new { error = true, data="Error", message= "No se pueden generar viajes en fechas anteriores a la de hoy"};

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
          join b in db.Barrio on id_barrio equals b.Id
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
            nombreBarrio = b.Nombre,
            v.IdResidente,
            v.Destino,
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

    //Listo los viajes existentes en el barrio del usuario
    [HttpGet("viajes/{fecha:DateTime}/{lat},{lng}")]
    public Object ListarViajesGeo(DateTime fecha, double lat, double lng)
    {
      try
      {

        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        var viajesPosibles = (
          from t in db.Trayecto
          let distance = (
              6371 *
              Math.Acos(
                  Math.Cos(DegreeToRadian(lat)) *
                  Math.Cos(DegreeToRadian(t.Latitud)) *
                  Math.Cos(
                      DegreeToRadian(t.Longitud) - DegreeToRadian(lng)
                  ) +
                  Math.Sin(DegreeToRadian(lat)) *
                  Math.Sin(DegreeToRadian(t.Latitud))
              )
          )
          where distance < 0.5
          group t by t.IdViaje into grupo
          select grupo.Key
        ).ToList();

        var viajes = (
          from v in db.Viaje
          join r in db.Residente on v.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join b in db.Barrio on id_barrio equals b.Id
          where u.IdBarrio == id_barrio && (v.Fecha.HasValue ? v.Fecha.Value.Date == fecha.Date : true) && viajesPosibles.Contains(v.Id)
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
            nombreBarrio = b.Nombre,
            v.IdResidente,
            v.Destino,
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
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
        var id_barrio = logueado.IdBarrio.Value;

        var solicitudes = (
          from s in db.SolicitudViaje
          join v in db.Viaje on s.IdViaje equals v.Id
          join r in db.Residente on v.IdResidente equals r.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join b in db.Barrio on id_barrio equals b.Id
          join es in db.EstadoSolicitud on s.IdEstadoSolicitud equals es.Id
          where s.IdResidente == residente.Id && (v.Fecha.HasValue ? v.Fecha.Value.Date >= DateTime.Now.Date : true)
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
            nombreBarrio = b.Nombre,
            v.IdResidente,
            v.Destino,
            estado = es.Descripcion,
            id_estado = es.Id,
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
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);

        var ofrecimientos = (
          from s in db.SolicitudViaje
          join r in db.Residente on s.IdResidente equals r.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          join b in db.Barrio on id_barrio equals b.Id
          join v in db.Viaje on s.IdViaje equals v.Id
          join es in db.EstadoSolicitud on s.IdEstadoSolicitud equals es.Id
          where v.IdResidente == residente.Id
          select new
          {
            s.Id,
            u.Avatar,
            p.Nombre,
            p.Apellido,
            v.IdDiaSemana,
            v.Fecha,
            v.Hora,
            v.SaleBarrio,
            v.Destino,
            nombreBarrio = b.Nombre,
            id_estado = es.Id,
            estado = es.Descripcion
          }
        );

        var agrupados = (
          from o in ofrecimientos
          group o by o.estado into grupo
          select new
          {
            estado = grupo.Key,
            grupo
          }
        );

        return new { error = false, data = agrupados };
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
          var viaje = db.Viaje.First(v => v.Id == solicitud.IdViaje && v.IdResidente == residente.Id);

          if (es.estado_solicitud == "Aceptada")
          {
            if (viaje.AutoAsientos <= 0)
            {
              throw new Exception("No hay asientos disponibles.");
            }

            viaje.AutoAsientos--;

            db.Viaje.Update(viaje);
            db.SaveChanges();
          }

          solicitud.IdEstadoSolicitud = estado.Id;

          db.SolicitudViaje.Update(solicitud);
          db.SaveChanges();

          transaction.Commit();

          this.push.enviarMensaje(residente.IdUsuario, "Se aceptó tu solicitud de viaje.");

          return new { error = false, data = es };
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", message = err.Message };
        }
      }
    }

    [HttpGet("barrio")]
    public Object InfoBarrio()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var barrio = (
          from b in db.Barrio
          where b.Id == id_barrio
          select b
        ).First();

        return new { error = false, data = barrio };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    private double DegreeToRadian(double angle)
    {
      return Math.PI * angle / 180.0;
    }
  }
}
