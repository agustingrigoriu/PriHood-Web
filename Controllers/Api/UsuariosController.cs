using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;

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

    // GET api/usuarios
    [HttpGet]
    public Object Get()
    {
      var usuarios = db.Usuario.ToList();

      return new { error = false, data = usuarios };
    }

    // GET api/usuarios/5
    [HttpGet("{id}")]
    public Object Get(int id)
    {
      var usuario = db.Usuario.First(u => u.Id == id);

      return new { error = false, data = usuario };
    }

    // POST api/values
    [HttpPost]
    public Object Post([FromBody]Usuario usuario)
    {
      db.Usuario.Add(usuario);
      db.SaveChanges();

      return new { error = false, data = usuario };
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]Usuario usuario)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public Object Delete(int id)
    {
      var usuario = db.Usuario.FirstOrDefault(u => u.Id == id);

      if (usuario != null)
      {
        db.Usuario.Remove(usuario);
        db.SaveChanges();

        return new { error = false, data = "Borrado correctamente." };
      }

      return new { error = true, data = "Error al borrar." };
    }
  }
}
