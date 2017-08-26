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
            tipo_servicio = ts.Descripcion,
            residente = p.Apellido + ", " + p.Nombre,
            rating = (int)Math.Round(pr.RatingTotal / pr.CantidadVotos), //Quizás acá debería hacer el cálculo del rating total, o desde la BD

          }
        ).ToList();

        return new { error = false, data = empleados };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpPost]
    public Object AgregarProveedor([FromBody]ModeloEmpleado me)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var logueado = HttpContext.Session.Authenticated();
          var persona = new Persona();
          persona.Apellido = me.apellido;
          persona.Nombre = me.nombre;
          persona.FechaNacimiento = me.fecha_nacimiento;
          persona.IdTipoDocumento = me.id_tipo_documento;
          persona.NroDocumento = me.numero_documento;
          persona.TelefonoMovil = me.telefono;
          db.Persona.Add(persona);

          db.SaveChanges();

          var usuario = new Usuario();

          usuario.Email = me.email;
          usuario.Password = auth.getHash(me.password); // hasheo le password
          usuario.IdPerfil = me.id_perfil.Value;
          usuario.IdBarrio = logueado.IdBarrio.Value;
          db.Usuario.Add(usuario);

          db.SaveChanges();

          var empleado = new Empleado();
          empleado.FechaInicioActividad = DateTime.Now;
          empleado.IdBarrio = logueado.IdBarrio.Value;
          empleado.IdPersona = persona.Id;
          empleado.IdUsuario = usuario.Id;
          db.Empleado.Add(empleado);

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
