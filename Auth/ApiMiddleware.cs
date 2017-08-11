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
      return this._next(context);
    }
  }
}