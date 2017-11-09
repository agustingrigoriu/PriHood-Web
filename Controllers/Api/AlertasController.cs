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
  public class AlertasController : Controller
  {
    private readonly PrihoodContext db;
    private AuthService auth;
    private PushService pushService;
    private EmailService emailService;
    public AlertasController(PrihoodContext context, AuthService a, PushService p, EmailService e)
    {
      db = context;
      auth = a;
      pushService = p;
      emailService = e;
    }

    [HttpPost]
    public Object GenerarAlerta([FromBody]Alertas alerta)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();

        if (!logueado.IdBarrio.HasValue) return new { error = true, data = "Su usuario no puede emitir alertas" };

        //Obtengo el residente, lo necesito para recuperar su Id
        Residente residente = db.Residente.First(r => r.IdUsuario == logueado.Id);

        alerta.IdResidente = residente.Id;
        alerta.Fecha = DateTime.Now;

        db.Alertas.Add(alerta);
        db.SaveChanges();


        //Envío de mail a encargados de seguridad y administradores
        var tipo_alerta = (
                  from ta in db.TipoAlerta
                  where ta.Id == alerta.IdTipoAlerta
                  select ta
         ).First();

        var empleados_barrio = (
          from u in db.Usuario
          join p in db.Perfil on u.IdPerfil equals p.Id
          where p.Descripcion == "Administrador" || p.Descripcion == "Encargado de Seguridad"
          select u.Email
        ).ToList();

        var persona_residente = (
          from p in db.Persona 
          join r in db.Residente on p.Id equals r.IdPersona
          where r.Id == alerta.IdResidente
          select p
        ).First();

        emailService.SendEmailAlert(tipo_alerta.Descripcion,alerta.Descripcion,persona_residente.Apellido + ", "+ persona_residente.Nombre,empleados_barrio);

        // this.pushService.enviarMensajeUsuariosBarrio(logueado.IdBarrio, "Alerta de \"" + tipo_alerta.Descripcion + "\".");

        return new { error = false, data = alerta };
      }
      catch (Exception e)
      {
        return new { error = true, data = e.InnerException.Message };
      }
    }

    [HttpPost("visto/{id_alerta}")]
    public Object MarcarAlertaVista(int id_alerta)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();

        Alertas alerta = db.Alertas.FirstOrDefault(a => a.Id == id_alerta);
        if (alerta == null) return new { error = true, data = "Alerta inexistente" };

        alerta.Visto = true;

        db.Alertas.Update(alerta);
        db.SaveChanges();

        return new { error = false, data = alerta };
      }
      catch (Exception e)
      {
        return new { error = true, data = e.InnerException.Message };
      }
    }


    [HttpGet]
    public Object ListarAlertas()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        var alertas = (
        from a in db.Alertas
        join ta in db.TipoAlerta on a.IdTipoAlerta equals ta.Id
        join r in db.Residente on a.IdResidente equals r.Id
        join p in db.Persona on r.IdPersona equals p.Id
        join u in db.Usuario on r.IdUsuario equals u.Id
        where u.IdBarrio == id_barrio
        orderby a.Fecha descending
        select new
        {
          id_alerta = a.Id,
          fecha = a.Fecha,
          descripcion = a.Descripcion,
          tipo_alerta = ta.Descripcion,
          id_tipo_alerta = a.IdTipoAlerta,
          id_residente = a.IdResidente,
          residente = p.Apellido + ", " + p.Nombre
        }
      );

        return new { error = false, data = alertas };
      }
      catch (Exception err)
      {
        return new
        {
          error = true,
          data = "fail",
          message = err.Message
        };
      }
    }

    [HttpGet("alertas_nuevas")]
    public Object ListarAlertasNuevas()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        var alertas = (
        from a in db.Alertas
        join ta in db.TipoAlerta on a.IdTipoAlerta equals ta.Id
        join r in db.Residente on a.IdResidente equals r.Id
        join p in db.Persona on r.IdPersona equals p.Id
        join u in db.Usuario on r.IdUsuario equals u.Id
        where u.IdBarrio == id_barrio && !a.Visto
        orderby a.Fecha descending
        select new
        {
          id_alerta = a.Id,
          fecha = a.Fecha,
          descripcion = a.Descripcion,
          tipo_alerta = ta.Descripcion,
          id_tipo_alerta = a.IdTipoAlerta,
          id_residente = a.IdResidente,
          residente = p.Apellido + ", " + p.Nombre
        }
      );

        return new { error = false, data = alertas };
      }
      catch (Exception err)
      {
        return new
        {
          error = true,
          data = "fail",
          message = err.Message
        };
      }
    }

  }
}
