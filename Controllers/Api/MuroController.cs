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
  public class MuroController : Controller
  {
    private readonly PrihoodContext db;
    public MuroController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet("publicaciones")]
    public Object ListarPublicaciones()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var publicaciones = (
          from p in db.Publicacion
          join m in db.Muro on p.IdMuro equals m.Id
          join u in db.Usuario on p.IdUsuario equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          where m.IdBarrio == id_barrio
          orderby p.Fecha, p.Hora descending
          select new
          {
            publicacion = p.Publicacion1,
            fecha = p.Fecha,
            hora = p.Hora,
            usuario = u.Email,
            perfil = pe.Descripcion
          }
        )
        .ToList();

        return new { error = false, data = publicaciones };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("comentarios/{id_publicacion}")]
    public Object ListarComentarios(int id_publicacion)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var comentarios = (
          from c in db.Comentario
          join p in db.Publicacion on c.IdPublicacion equals p.Id
          join u in db.Usuario on c.IdUsuario equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          where c.IdPublicacion == id_publicacion
          orderby p.Fecha, p.Hora descending
          select new
          {
            comentario = c.Comentario1,
            fecha = c.Fecha,
            hora = c.Hora,
            usuario = u.Email,
            perfil = pe.Descripcion
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


    [HttpPost]
    public Object RegistrarPublicacion([FromBody]ModeloPublicacion mp)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var id_barrio = logueado.IdBarrio.Value;

          var muro = db.Muro.First(m => m.IdBarrio == id_barrio);

          var publicacion = new Publicacion();
          publicacion.Publicacion1 = mp.publicacion;
          publicacion.IdMuro = muro.Id;
          publicacion.Fecha = mp.fecha;
          publicacion.Hora = mp.hora;
          publicacion.IdUsuario = mp.id_usuario;
          db.Publicacion.Add(publicacion);

          db.SaveChanges();

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

    [HttpPost]
    public Object RegistrarComentario([FromBody]ModeloComentario mc)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();

          var comentario = new Comentario();
          comentario.Comentario1 = mc.comentario;
          comentario.IdPublicacion = mc.id_publicacion;
          comentario.Fecha = mc.fecha;
          comentario.Hora = mc.hora;
          comentario.IdUsuario = mc.id_usuario;
          db.Comentario.Add(comentario);

          db.SaveChanges();

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
