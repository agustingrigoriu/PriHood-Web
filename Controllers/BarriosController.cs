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

    [HttpGet]
    public IEnumerable<Barrio> Get()
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Barrio.ToList();
      }
    }

    [HttpGet("{id}")]
    public Barrio Get(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Barrio.First(u => u.Id == id);
      }
    }

    [HttpPost]
    public void Post([FromBody]JObject value)
    {
      Barrio posted = value.ToObject<Barrio>();
      using (PrihoodContext db = new PrihoodContext())
      {
        db.Barrio.Add(posted);
        db.SaveChanges();
      }
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody]JObject value)
    {
      Barrio posted = value.ToObject<Barrio>();
      posted.Id = id;
      using (PrihoodContext db = new PrihoodContext())
      {
        db.Barrio.Update(posted);
        db.SaveChanges();
      }
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        if (db.Barrio.Where(t => t.Id == id).Count() > 0) // Check if element exists
          db.Barrio.Remove(db.Barrio.First(t => t.Id == id));
        db.SaveChanges();
      }
    }
  }
}
