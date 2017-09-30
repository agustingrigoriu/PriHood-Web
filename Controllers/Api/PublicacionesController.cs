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
    private PushService pushService;
    public PublicacionesController(PrihoodContext context, AuthService a, PushService p)
    {
      db = context;
      auth = a;
      pushService = p;
    }

    [HttpPost("publicar/{directo?}")]
    public Object CrearPublicacion([FromBody]Publicacion publicacion, string directo)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();

        publicacion.IdPersonal = logueado.Id;
        publicacion.Fecha = DateTime.Now;

        if (directo == null)
        {
          publicacion.IdResidente = null;
        }
        else if (directo == "directo")
        {
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
          publicacion.IdResidente = residente.Id;
        }

        db.Publicacion.Add(publicacion);
        db.SaveChanges();

        return new { error = false, data = publicacion };
      }
      catch (Exception)
      {
        return new { error = true, data = "fail" };
      }
    }

    [HttpGet("{id_publicacion:int}")]
    public Object GetPublicacion(int id_publicacion)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var publicacion = (
          from p in db.Publicacion
          join u in db.Usuario on p.IdPersonal equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          where u.IdBarrio == id_barrio && p.IdResidente == null && p.Id == id_publicacion
          orderby p.Fecha descending
          select new
          {
            p.Id,
            p.Titulo,
            p.Texto,
            p.Fecha,
            perfil = pe.Descripcion
          }
        ).First();

        return new { error = false, data = publicacion };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("{id_publicacion:int}/privada")]
    public Object GetMensaje(int id_publicacion)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var publicacion = (
          from p in db.Publicacion
          join u in db.Usuario on p.IdPersonal equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          join r in db.Residente on p.IdResidente equals r.Id
          join ps in db.Persona on r.IdPersona equals ps.Id
          join re in db.Residencia on r.IdResidencia equals re.Id
          where u.IdBarrio == id_barrio && p.IdResidente != null && p.Id == id_publicacion
          orderby p.Fecha descending
          select new
          {
            p.Id,
            p.Titulo,
            p.Texto,
            p.Fecha,
            perfil = pe.Descripcion,
            residente = ps,
            residencia = re
          }
        ).First();

        return new { error = false, data = publicacion };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("listado/{privadas?}")]
    public Object ListarPublicaciones(string privadas)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        if (privadas == "privadas")
        {
          var publicaciones = (
          from p in db.Publicacion
          join u in db.Usuario on p.IdPersonal equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          join r in db.Residente on p.IdResidente equals r.Id
          join ps in db.Persona on r.IdPersona equals ps.Id
          join re in db.Residencia on r.IdResidencia equals re.Id
          where u.IdBarrio == id_barrio && p.IdResidente != null
          orderby p.Fecha descending
          select new
          {
            p.Id,
            p.Titulo,
            p.Texto,
            p.Fecha,
            perfil = pe.Descripcion,
            residente = ps,
            residencia = re
          }
        );

          return new { error = false, data = publicaciones };
        }
        else
        {
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
            id = c.Id,
            comentario = c.Texto,
            fecha = c.Fecha,
            usuario = u.Email,
            perfil = pe.Descripcion,
            sent_by_me = logueado.Id == c.IdUsuario
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
          var publicacion = db.Publicacion.First(p => p.Id == id_publicacion);

          comentario.IdPublicacion = publicacion.Id;
          comentario.Fecha = DateTime.Now;
          comentario.IdUsuario = logueado.Id;
          db.Comentario.Add(comentario);

          db.SaveChanges();

          var empleado = db.Empleado.FirstOrDefault(e => e.IdUsuario == logueado.Id);

          if (publicacion.IdResidente != null && empleado != null)
          {
            var residente = db.Residente.First(r => r.Id == publicacion.IdResidente);

            this.pushService.enviarMensaje(residente.IdUsuario, "La administración respondió a tu mensaje sobre \"" + publicacion.Titulo + "\".");
          }

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

    [HttpDelete("{id_publicacion}/borrar")]
    public Object BorrarPublicacion(int id_publicacion)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();

          var publicacion = (
            from p in db.Publicacion
            join u in db.Usuario on p.IdPersonal equals u.Id
            where p.Id == id_publicacion && u.IdBarrio == logueado.IdBarrio
            select p
          ).First();
          var comentarios = (
            from c in db.Comentario
            where c.IdPublicacion == id_publicacion
            select c
          );

          db.Comentario.RemoveRange(comentarios);
          db.Publicacion.Remove(publicacion);
          db.SaveChanges();

          transaction.Commit();

          return new { error = false, data = "ok" };
        }
        catch (Exception)
        {
          transaction.Rollback();
          return new { error = true, data = "fail" };
        }
      }
    }

    [HttpGet("privadas")]
    public Object ListarPublicacionesPrivadas()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
        var publicaciones = (
          from p in db.Publicacion
          join u in db.Usuario on p.IdPersonal equals u.Id
          join pe in db.Perfil on u.IdPerfil equals pe.Id
          where u.IdBarrio == id_barrio && p.IdResidente == residente.Id
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
  }
}
