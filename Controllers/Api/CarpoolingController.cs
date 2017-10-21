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
    public CarpoolingController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
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
    [HttpGet()]
    public Object ListarViajes()
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
          join t in db.Trayecto on v.Id equals t.IdViaje
          where u.IdBarrio == id_barrio
          select new
          {
            id_viaje = v.Id,
            auto_modelo = v.AutoModelo,
            auto_color = v.AutoColor,
            auto_asientos = v.AutoAsientos,
            auto_patente = v.AutoPatente,
            fecha = v.Fecha,
            observaciones = v.Observaciones,
            dia_semana = v.IdDiaSemana,
            sale_barrio = v.SaleBarrio,
            id_residente = v.IdResidente,
            residente = p.Apellido + ", " + p.Nombre,
            usuario = u.Email,
            trayecto = t
          }
        ).ToList();

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
            trayectos = (
              from tr in db.Trayecto
              where tr.IdViaje == v.Id
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

  }

}
