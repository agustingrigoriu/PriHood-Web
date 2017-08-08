using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;

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

    [HttpGet]
    public Object Get()
    {
      return new { error = false, data = db.Residencia.ToList() };
    }

    [HttpGet("{id}")]
    public Object Get(int id)
    {
      return new { error = false, data = db.Residencia.FirstOrDefault(u => u.Id == id) };
    }

    [HttpPost]
    public Object Post([FromBody]Residencia re)
    {
      db.Residencia.Add(re);
      db.SaveChanges();

      return new { error = false, data = "ok" };
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
}
