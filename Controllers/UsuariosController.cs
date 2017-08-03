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
    // GET api/usuarios
    [HttpGet]
    public IEnumerable<Usuario> Get()
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Usuario.ToList();
      }
    }

    // GET api/usuarios/5
    [HttpGet("{id}")]
    public Usuario Get(int id)
    {
      using (PrihoodContext db = new PrihoodContext())
      {
        return db.Usuario.First(u => u.Id == id);
      }
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
