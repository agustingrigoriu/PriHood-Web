using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Microsoft.AspNetCore.Http;

namespace PriHood.Auth
{
  public class ApiMiddleware
  {
    private PrihoodContext _db;
    private readonly RequestDelegate _next;
    private string[] noProtegidos = new string[] { "/api/login", "/api/token" };

    public ApiMiddleware(RequestDelegate next, PrihoodContext db)
    {
      _next = next;
      _db = db;
    }

    public Task Invoke(HttpContext context)
    {
      bool isLogin = context.Session.Authenticated() != null;

      if (context.Request.Method.ToUpper() == "OPTIONS")
      {
        context.Response.StatusCode = 200;
        return context.Response.WriteAsync("ok");
      }

      if (context.Request.Path.StartsWithSegments("/api"))
      {
        foreach (var path in noProtegidos)
        {
          if (context.Request.Path.StartsWithSegments(path))
          {
            return this._next(context);
          }
        }

        if (!isLogin)
        {
          context.Response.StatusCode = 401;
          return context.Response.WriteAsync("sin permisos");
        }
      }

      return this._next(context);
    }
  }
}