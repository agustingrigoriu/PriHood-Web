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

    [HttpGet]
    public IEnumerable<Residencia> Get()
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Residencia.ToList();
      }
    }

    [HttpGet("{id}")]
    public Residencia Get(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Residencia.First(u => u.Id == id);
      }
    }

    [HttpPost]
    public void Post([FromBody]JObject value)
    {
      Residencia posted = value.ToObject<Residencia>();
      using (PrihoodContext db = new PrihoodContext())
      {
        db.Residencia.Add(posted);
        db.SaveChanges();
      }
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody]JObject value)
    {
      Residencia posted = value.ToObject<Residencia>();
      posted.Id = id;
      using (PrihoodContext db = new PrihoodContext())
      {
        db.Residencia.Update(posted);
        db.SaveChanges();
      }
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        if (db.Residencia.Where(t => t.Id == id).Count() > 0) // Check if element exists
          db.Residencia.Remove(db.Residencia.First(t => t.Id == id));
        db.SaveChanges();
      }
    }
  }
}
