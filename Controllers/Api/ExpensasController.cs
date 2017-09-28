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
using Microsoft.AspNetCore.Http;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class ExpensasController : Controller
  {
    private readonly PrihoodContext db;
    private readonly UploadService uploadService;
    public ExpensasController(PrihoodContext context, UploadService ul)
    {
      db = context;
      uploadService = ul;
    }

    [HttpPost]
    public Object AgregarExpensa(ModeloExpensas me)
    {
      using (var transaction = db.Database.BeginTransaction())
      {
        try
        {
          var url_expensa = this.uploadService.UploadFile(me.file);
          var logueado = HttpContext.Session.Authenticated();

          var expensas = new Expensas();
          expensas.IdResidencia = me.id_residencia;
          expensas.FechaExpensa = me.fecha_expensa;
          expensas.FechaTransaccion = DateTime.Now;
          expensas.FechaVencimiento = me.fecha_vencimiento;
          expensas.Monto = me.monto;
          expensas.Pagado = me.pagado;
          expensas.Observaciones = me.observaciones;
          expensas.UrlExpensa = url_expensa;
          db.Expensas.Add(expensas);

          db.SaveChanges();

          transaction.Commit();

          return new { error = false, data = expensas };
        }
        catch (Exception err)
        {
          transaction.Rollback();
          return new { error = true, data = err.Message };
        }
      }
    }

    [HttpGet("residencias")]
    public Object ObtenerExpensas()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var expensas = (
          from e in db.Expensas
          join r in db.Residencia on e.IdResidencia equals r.Id
          select new
          {
            id_expensas = e.Id,
            fecha_expensa = e.FechaExpensa,
            fecha_transaccion = e.FechaTransaccion,
            fecha_vencimiento = e.FechaVencimiento,
            monto = e.Monto,
            pagado = e.Pagado,
            observaciones = e.Observaciones,
            url_expensa = e.UrlExpensa,
            id_residencia = r.Id,
            residencia = r.Nombre
          }
        ).ToList();

        return new { error = false, data = expensas };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet("residencias/{id_residencia}")]
    public Object ObtenerExpensasPorResidenciaAdmin(int id_residencia)
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();
        var id_barrio = logueado.IdBarrio.Value;
        var expensas = (
          from e in db.Expensas
          join r in db.Residencia on e.IdResidencia equals r.Id
          where r.Id == id_residencia
          select new
          {
            id_expensas = e.Id,
            fecha_expensa = e.FechaExpensa,
            fecha_transaccion = e.FechaTransaccion,
            fecha_vencimiento = e.FechaVencimiento,
            monto = e.Monto,
            pagado = e.Pagado,
            observaciones = e.Observaciones,
            url_expensa = e.UrlExpensa,
            id_residencia = r.Id,
            residencia = r.Nombre
          }
        ).ToList();

        return new { error = false, data = expensas };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

    [HttpGet]
    public Object ObtenerExpensasPorResidencia()
    {
      try
      {
        var logueado = HttpContext.Session.Authenticated();

        var residencia = (
          from u in db.Usuario
          join r in db.Residente on u.Id equals r.IdUsuario
          join re in db.Residencia on r.IdResidencia equals re.Id
          where u.Id == logueado.Id
          select re
        ).FirstOrDefault();

        if (residencia == null) return new { error = true, data = "fail", message = "Su usuario no posee residencia" };

        var expensas = (
          from e in db.Expensas
          join r in db.Residencia on e.IdResidencia equals r.Id
          where r.Id == residencia.Id
          select new
          {
            id_expensas = e.Id,
            fecha_expensa = e.FechaExpensa,
            fecha_transaccion = e.FechaTransaccion,
            fecha_vencimiento = e.FechaVencimiento,
            monto = e.Monto,
            pagado = e.Pagado,
            observaciones = e.Observaciones,
            url_expensa = e.UrlExpensa,
            id_residencia = r.Id,
            residencia = r.Nombre
          }
        ).ToList();

        return new { error = false, data = expensas };

      }
      catch (Exception err)
      {
        return new { error = true, data = "fail", message = err.Message };
      }
    }

  }
}
