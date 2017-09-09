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
  public class AmenitiesController : Controller
  {
    private readonly PrihoodContext db;
    public AmenitiesController(PrihoodContext context)
    {
      db = context;
    }

    [HttpPost]
    public Object CrearAmenity([FromBody]Amenity amenity)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        amenity.IdBarrio = id_barrio;

        db.Amenity.Add(amenity);
        db.SaveChanges();

        return new { error = false, data = amenity };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpPut("{id_amenity}")]
    public Object ActualizarAmenity(int id_amenity, [FromBody]Amenity amenity)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;

        amenity.Id = id_amenity;
        db.Amenity.Update(amenity);
        db.SaveChanges();

        return new { error = false, data = amenity };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }

    [HttpGet("{id_tipo_amenity?}")]
    public Object ListarAmenities(int? id_tipo_amenity)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var tipos = (
          from a in db.Amenity
          join ta in db.TipoAmenity on a.IdTipoAmenity equals ta.Id
          where a.IdBarrio == id_barrio && ((id_tipo_amenity.HasValue && a.IdTipoAmenity == id_tipo_amenity.Value) || !id_tipo_amenity.HasValue)
          select new
          {
            id = a.Id,
            nombre = a.Nombre,
            descripcion = a.Descripcion,
            telefono = a.Telefono,
            ubicacion = a.Ubicacion,
            tipo_amenity = ta.Descripcion
          }
        ).ToList();

        return new { error = false, data = tipos };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("amenitie/{id_amenity}")]
    public Object ObtenerAmenity(int id_amenity)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio;
        var amenity = (
          from a in db.Amenity
          join ta in db.TipoAmenity on a.IdTipoAmenity equals ta.Id
          where a.Id == id_amenity && a.IdBarrio == id_barrio
          select new
          {
            id = a.Id,
            nombre = a.Nombre,
            descripcion = a.Descripcion,
            telefono = a.Telefono,
            ubicacion = a.Ubicacion,
            id_tipo_amenity = a.IdTipoAmenity,
            tipo_amenity = ta.Descripcion
          }
        ).FirstOrDefault();

        if (amenity == null) return new { error = true, data = "No existe ese amenity" };

        return new { error = false, data = amenity };
      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpDelete("{id_amenity}")]
    public Object EliminarAmenity(int id_amenity)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio;

        if (db.Amenity.Where(a => a.Id == id_amenity).Count() > 0)
        {
          var amenity = db.Amenity.First(t => t.Id == id_amenity);
          db.Amenity.Remove(amenity);
          db.SaveChanges();
          return new { error = false, data = amenity };
        }

        return new { error = false, data = "No existe ese amenity" };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }



    // [HttpGet("{id_amenity}")]
    // public Object ListarReservasParaAmenity(int id_amenity)
    // {
    //   try
    //   {
    //     var logueado = HttpContext.Session.Authenticated();
    //     var id_barrio = logueado.IdBarrio.Value;
    //     var tipos = (
    //       from a in db.Amenity
    //       join t in db.Turno on 
    //       where a.IdBarrio == id_barrio && a.Id == id_amenity
    //       select new
    //       {
    //         a.Descripcion,
    //         a.Id,
    //         a.IdTipoAmenity,
    //         a.Nombre,
    //         a.Ubicacion
    //       }
    //     ).ToList();

    //     return new { error = false, data = tipos };
    //   }
    //   catch (Exception err)
    //   {
    //     return new { error = true, data = "fail", message = err.Message };
    //   }
    // }


  }
}
