using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;

namespace PriHood.Auth
{
  public class PushService
  {
    private PrihoodContext _db;
    public PushService(PrihoodContext db)
    {
      this._db = db;
    }

    public void enviarMensaje(int usuario_id, string message)
    {
      var usuario = _db.Usuario.First(u => u.Id == usuario_id);
      enviarMensaje(usuario, message);
    }

    public void enviarMensajeUsuariosBarrio(int id_barrio, string message)
    {
      var usuarios = (
        from u in _db.Usuario
        where u.IdBarrio == id_barrio
        select u
        ).ToList();

      foreach (var usuario in usuarios)
      {
        enviarMensaje(usuario, message);
      }
    }
    async public void enviarMensaje(Usuario usuario, string message)
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Add("authorization", "Basic ZmI4ZmJiZTMtYjQ2Mi00NzI0LTkxMzQtNmUxMDA4ZTBjMGU3");
        var obj = new
        {
          app_id = "8f473da7-dec3-47be-ad34-e2a4988b6587",
          include_player_ids = new List<string>() { usuario.Token },
          contents = new { en = message }
        };

        await client.PostAsync("https://onesignal.com/api/v1/notifications", new JsonContent(obj));
      }

    }

  }

  public class JsonContent : StringContent
  {
    public JsonContent(object obj) :
        base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
    { }
  }
}