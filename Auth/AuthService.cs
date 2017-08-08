using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;

namespace PriHood.Auth
{
  public class AuthService
  {
    private PrihoodContext _db;
    public AuthService(PrihoodContext db)
    {
      this._db = db;
    }

    public Usuario Login(string email, string password)
    {
      return _db.Usuario.FirstOrDefault(u => u.Email == email && u.Password == getHash(password));
    }

    public Usuario Register(Usuario usuario)
    {
      usuario.Password = getHash(usuario.Password);

      var existe = _db.Usuario.FirstOrDefault(u => u.Email == usuario.Email);

      if (existe == null)
      {
        _db.Usuario.Add(usuario);
        _db.SaveChanges();
        
        return usuario;
      }

      return null;
    }

    private static string getHash(string text)
    {
      using (var sha256 = SHA256.Create())
      {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower().Substring(0, 30);
      }
    }
  }
}