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
  public class PublicacionesController : Controller
  {
    private readonly PrihoodContext db;
    private AuthService auth;
    public PublicacionesController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
    }

    [HttpPost("publicar")]
    public Object CrearPublicacion([FromBody]Publicacion publicacion)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();

        publicacion.IdPersonal = logueado.Id;
        publicacion.Fecha = DateTime.Now;
        publicacion.IdResidente = null;

        db.Publicacion.Add(publicacion);
        db.SaveChanges();

        return new { error = false, data = publicacion };
      }
      catch (Exception)
      {
        return new { error = true, data = "fail" };
      }
    }


    [HttpGet("listado")]
    public Object ListarPublicaciones()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var publicaciones = (
          from p in db.Publicacion
          join u in db.Usuario on p.IdPersonal equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          where u.IdBarrio == id_barrio && p.IdResidente == null
          orderby p.Fecha descending
          select new
          {
            p.Id,
            p.Titulo,
            p.Texto,
            p.Fecha,
            perfil = pe.Descripcion
          }
        );

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
          orderby p.Fecha descending
          select new
          {
            comentario = c.Texto,
            fecha = c.Fecha,
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

    [HttpPost("{id_publicacion}/comentar")]
    public Object RegistrarComentario(int id_publicacion, [FromBody]Comentario comentario)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();

          comentario.IdPublicacion = id_publicacion;
          comentario.Fecha = DateTime.Now;
          comentario.IdUsuario = logueado.Id;
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
