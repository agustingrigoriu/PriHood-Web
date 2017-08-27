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

    [HttpGet]
    public Object ListarProveedores()
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
          where r.Id == id_barrio
          select new
          {
            nombre = pr.Nombre,
            descripcion = pr.Descripcion,
            telefono = pr.Telefono,
            tipo_servicio = ts.Descripcion,
            residente_recomienda = p.Apellido + ", " + p.Nombre,
            rating = (int)Math.Round((decimal)pr.RatingTotal / (decimal)pr.CantidadVotos), //Quizás acá debería hacer el cálculo del rating total, o desde la BD
            avatar = pr.Avatar,
          }
        ).ToList();

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
          var proveedor = new Proveedor();
          proveedor.Nombre = me.nombre;
          proveedor.Descripcion = me.descripcion;
          proveedor.Telefono = me.telefono;
          proveedor.Avatar = me.avatar;
          proveedor.IdResidenteRecomienda = me.id_residente_recomienda;
          proveedor.IdTipoServicio = me.id_tipo_servicio;
          proveedor.CantidadVotos = 1;
          proveedor.RatingTotal = me.rating;
          db.Proveedor.Add(proveedor);

          db.SaveChanges();

          var registro_votos = new RegistroVotos();
          registro_votos.Fecha = DateTime.Today;
          registro_votos.IdProveedor = proveedor.Id;
          registro_votos.IdResidente = proveedor.IdResidenteRecomienda;
          db.RegistroVotos.Add(registro_votos);

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

    [HttpPost("votar")]
    public Object VotarProveedor([FromBody]ModeloVoto mv)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();

          //Un usuario no puede votar dos veces al mismo proveedor, realizo control
          if (db.RegistroVotos.Count(r => r.IdResidente == mv.id_residente_vota) > 0)
            return new { error = true, data = "Ya ha votado a este proveedor anteriormente" };

          var proveedor = db.Proveedor.First(p => p.Id == mv.id_proveedor);
          proveedor.CantidadVotos += 1;
          proveedor.RatingTotal += mv.rating;

          db.Proveedor.Update(proveedor);
          db.SaveChanges();

          var registro_votos = new RegistroVotos();
          registro_votos.Fecha = DateTime.Today;
          registro_votos.IdProveedor = proveedor.Id;
          registro_votos.IdResidente = mv.id_residente_vota;
          db.RegistroVotos.Add(registro_votos);

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
