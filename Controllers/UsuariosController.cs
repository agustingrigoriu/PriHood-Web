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
  public class UsuariosController : Controller
  {

    [HttpGet]
    public IEnumerable<Usuario> Get()
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Usuario.ToList();
      }
    }

    [HttpGet("{id}")]
    public Usuario Get(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Usuario.First(u => u.Id == id);
      }
    }

    [HttpPost]
    public void Post([FromBody]JObject value)
    {
      Usuario posted = value.ToObject<Usuario>();
      using (PrihoodContext db = new PrihoodContext())
      {
        db.Usuario.Add(posted);
        db.SaveChanges();
      }
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody]JObject value)
    {
      Usuario posted = value.ToObject<Usuario>();
      posted.Id = id;
      using (PrihoodContext db = new PrihoodContext())
      {
        db.Usuario.Update(posted);
        db.SaveChanges();
      }
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        if (db.Usuario.Where(t => t.Id == id).Count() > 0) // Check if element exists
          db.Usuario.Remove(db.Usuario.First(t => t.Id == id));
        db.SaveChanges();
      }
    }
  }
}
