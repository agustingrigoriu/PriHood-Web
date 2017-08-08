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
    private readonly PrihoodContext db;
    public UsuariosController(PrihoodContext context)
    {
      db = context;
    }

    [HttpGet]
    public Object Get()
    {
      return new { error = false, data = db.Usuario.ToList() };
    }

    [HttpGet("{id}")]
    public Object Get(int id)
    {
      return new { error = false, data = db.Usuario.First(u => u.Id == id) };
    }

    [HttpPost]
    public Object Post([FromBody]Usuario usuario)
    {
      db.Usuario.Add(usuario);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpPut("{id}")]
    public Object Put(int id, [FromBody]Usuario usuario)
    {
      db.Usuario.Update(usuario);
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }

    [HttpDelete("{id}")]
    public Object Delete(int id)
    {
      if (db.Usuario.Where(t => t.Id == id).Count() > 0) // Check if element exists
        db.Usuario.Remove(db.Usuario.First(t => t.Id == id));
      db.SaveChanges();

      return new { error = false, data = "ok" };
    }
  }
}
