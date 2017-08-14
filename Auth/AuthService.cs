using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Microsoft.EntityFrameworkCore;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

namespace PriHood.Auth
{
  public class AuthService
  {
    private string _secret = "MY-SECRET-KEY";
    private PrihoodContext _db;
    public AuthService(PrihoodContext db)
    {
      this._db = db;
    }

    public Usuario Login(string email, string password)
    {
      return _db.Usuario.Where(u => u.Email == email && u.Password == getHash(password)).FirstOrDefault();
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

    public string getToken(Usuario usuario)
    {
      var algorithm = new HMACSHA256Algorithm();
      var serializer = new JsonNetSerializer();
      var urlEncoder = new JwtBase64UrlEncoder();
      var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

      var token = encoder.Encode(new { id = usuario.Id, date = DateTime.Now.ToString() }, this._secret);

      return token;
    }

    public Usuario token2usuario(string token)
    {
      try
      {
        var serializer = new JsonNetSerializer();
        var provider = new UtcDateTimeProvider();
        var validator = new JwtValidator(serializer, provider);
        var urlEncoder = new JwtBase64UrlEncoder();
        var decoder = new JwtDecoder(serializer, validator, urlEncoder);

        var json = decoder.DecodeToObject<IDictionary<string, object>>(token, this._secret, verify: true);
        var usuario = this._db.Usuario.Where(u => u.Id == Int32.Parse(json["id"].ToString())).FirstOrDefault();

        return usuario;
      }
      catch (System.Exception)
      {
        return null;
      }
    }

    public string getHash(string text)
    {
      using (var sha256 = SHA256.Create())
      {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower().Substring(0, 30);
      }
    }
  }
}