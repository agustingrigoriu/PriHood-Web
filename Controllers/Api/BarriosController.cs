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
  public class BarriosController : Controller
  {

    private readonly PrihoodContext db;

    public BarriosController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet]
    public Object Get()
    {
      return new { error = false, data = db.Barrio.ToList() };
    }


    [HttpGet("{id}")]
    public Object Get(int id)
    {
      return new { error = false, data = db.Barrio.FirstOrDefault(u => u.Id == id) };
    }

    [HttpPost]
    public Object Post([FromBody]Barrio barrio)
    {
      db.Barrio.Add(barrio);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpPost("codigo")]
    public Object CodigoPorBarrio([FromBody]String codigo)
    {
      return new { error = false, data = db.Barrio.FirstOrDefault(u => u.Codigo == codigo) };
    }

    [HttpPut("{id}")]
    public Object Put(int id, [FromBody]Barrio barrio)
    {
      barrio.Id = id;
      db.Barrio.Update(barrio);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpDelete("{id}")]
    public Object Delete(int id)
    {
      if (db.Barrio.Where(t => t.Id == id).Count() > 0) // Check if element exists
        db.Barrio.Remove(db.Barrio.First(t => t.Id == id));

      db.SaveChanges();

      return new { error = false, data = "ok" };
    }
  }
}
