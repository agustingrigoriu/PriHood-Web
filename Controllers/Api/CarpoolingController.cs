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
    public Object RegistrarViaje([FromBody]Viaje viaje, [FromBody]Trayecto trayecto)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          db.Viaje.Add(viaje);
          db.SaveChanges();

          trayecto.IdViaje = viaje.Id;

          db.Trayecto.Add(trayecto);
          db.SaveChanges();

          transaction.Commit();
          return new { error = false, data = viaje };

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

  }

}
