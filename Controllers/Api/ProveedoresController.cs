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
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class ProveedoresController : Controller
  {
    private readonly PrihoodContext db;
    private AuthService auth;
    public ProveedoresController(PrihoodContext context, AuthService a)
    {
      db = context;
      auth = a;
    }


    [HttpGet("{id_tipo_servicio?}")]
    public Object ListarProveedoresPorTipoServicio(int? id_tipo_servicio)
    {
      try
      {
        //Obtengo todos los proveedores recomendados por alguien perteneciente al barrio del usuario
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var proveedores = (
          from pr in db.Proveedor
          join r in db.Residente on pr.IdResidenteRecomienda equals r.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join ts in db.TipoServicio on pr.IdTipoServicio equals ts.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          where u.IdBarrio == id_barrio && ((id_tipo_servicio.HasValue && pr.IdTipoServicio == id_tipo_servicio.Value) || !id_tipo_servicio.HasValue)
          select new
          {
            id = pr.Id,
            nombre = pr.Nombre,
            descripcion = pr.Descripcion,
            telefono = pr.Telefono,
            direccion = pr.Direccion,
            tipo_servicio = ts.Descripcion,
            residente_recomienda = p.Apellido + ", " + p.Nombre,
            rating = pr.CantidadVotos > 0 ? pr.RatingTotal / pr.CantidadVotos : 0,
            avatar = pr.Avatar
          }
        );

        return new { error = false, data = proveedores };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpPost]
    public Object AgregarProveedor([FromBody]ModeloProveedor me)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
          var proveedor = new Proveedor();
          proveedor.Nombre = me.nombre;
          proveedor.Descripcion = me.descripcion;
          proveedor.Telefono = me.telefono;
          proveedor.Direccion = me.direccion;
          proveedor.Avatar = me.avatar;
          proveedor.IdResidenteRecomienda = residente.Id;
          proveedor.IdTipoServicio = me.id_tipo_servicio;
          proveedor.CantidadVotos = 0;
          proveedor.RatingTotal = 0;
          db.Proveedor.Add(proveedor);

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

    [HttpPost("{id_proveedor}/votar")]
    public Object VotarProveedor(int id_proveedor, [FromBody]ModeloVoto mv)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var residente = db.Residente.First(r => r.IdUsuario == logueado.Id);
          var votos = db.RegistroVotos.Count(r => r.IdResidente == residente.Id && r.IdProveedor == id_proveedor);

          //Un usuario no puede votar dos veces al mismo proveedor, realizo control
          if (votos > 0)
            return new { error = true, data = "Ya ha votado a este proveedor anteriormente" };

          var proveedor = db.Proveedor.First(p => p.Id == id_proveedor);
          proveedor.CantidadVotos += 1;
          proveedor.RatingTotal += mv.rating;

          db.Proveedor.Update(proveedor);
          db.SaveChanges();

          var registro_votos = new RegistroVotos();
          registro_votos.Fecha = DateTime.Today;
          registro_votos.IdProveedor = proveedor.Id;
          registro_votos.IdResidente = residente.Id;
          registro_votos.Comentario = mv.comentario;
          registro_votos.Rating = mv.rating;
          db.RegistroVotos.Add(registro_votos);

          db.SaveChanges();

          transaction.Commit();
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = "Error", err };
        }
      }

      return new { error = false, data = "ok" };
    }

    [HttpGet("{id_proveedor}/comentarios")]
    public Object ListarComentarios(int id_proveedor)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var comentarios = (
          from rv in db.RegistroVotos
          join r in db.Residente on rv.IdResidente equals r.Id
          join p in db.Persona on r.IdPersona equals p.Id
          join u in db.Usuario on r.IdUsuario equals u.Id
          where u.IdBarrio == id_barrio && rv.IdProveedor == id_proveedor
          select new
          {
            nombre = p.Nombre,
            apellido = p.Apellido,
            avatar = u.Avatar,
            rating = rv.Rating,
            mensaje = rv.Comentario,
            fecha = rv.Fecha
          }
        ).ToList();

        return new { error = false, data = comentarios };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }
  }

}
