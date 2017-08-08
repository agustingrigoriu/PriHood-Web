using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PriHood.Models;


namespace PriHood.Auth
{
  public static class SessionExtensions
  {
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
      session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
      var value = session.GetString(key);

      return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }

    public static void LogInAccount(this ISession session, Usuario usuario)
    {
      session.SetObjectAsJson("_usuario", usuario);
    }

    public static void LogOutAccount(this ISession session)
    {
      session.Remove("_usuario");
    }

    public static Usuario Authenticated(this ISession session)
    {
      return session.GetObjectFromJson<Usuario>("_usuario");
    }
  }
}
