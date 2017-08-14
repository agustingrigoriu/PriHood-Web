using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class ResidenciasController : Controller
  {

    private readonly PrihoodContext db;
    public ResidenciasController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet("barrio/{id}")]
    public Object GetResidenciasPorBarrio(int id)
    {
      var residencias = db.Residencia.Where(r => r.IdBarrio == id).ToList();
      
      return new { error = false, data = residencias };
    }

    [HttpGet("{id}")]
    public Object GetResidencia(int id)
    {
      return new { error = false, data = db.Residencia.FirstOrDefault(u => u.Id == id) };
    }

    [HttpPost("codigo")]
    public Object CodigoPorResidencia([FromBody]ModeloCodigo obj)
    {
      var residencia = db.Residencia.Where(u => u.Codigo.Equals(obj.codigo_residencia))
                                    .Include("IdBarrioNavigation")
                                    .FirstOrDefault();
      if (residencia == null)
        return new { error = true, data = "empty" };
      return new { error = false, data = residencia };
    }

    [HttpPost]
    public Object Post([FromBody]Residencia re)
    {
      try
      {
        var guid = Guid.NewGuid();
        var codigo = guid.ToString().Substring(0, 6);

        re.Codigo = codigo;

        db.Residencia.Add(re);
        db.SaveChanges();

        return new { error = false, data = "ok" };
      }
      catch (System.Exception)
      {
        return new { error = true, data = null as string };
      }
    }

    [HttpPut("{id}")]
    public Object Put(int id, [FromBody]Residencia res)
    {
      res.Id = id;

      db.Residencia.Update(res);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpDelete("{id}")]
    public Object Delete(int id)
    {
      if (db.Residencia.Where(t => t.Id == id).Count() > 0) // Check if element exists
        db.Residencia.Remove(db.Residencia.First(t => t.Id == id));
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }
  }

  public class ModeloCodigo
  {
    public string codigo_residencia { get; set; }
  }
}
