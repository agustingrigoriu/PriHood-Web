using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriHood.Models;
using Newtonsoft.Json.Linq;
using PriHood.Auth;
using System.Security.Cryptography;
using System.Text;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.AspNetCore.Http;

namespace PriHood.Controllers
{
  [Route("api/[controller]")]
  public class FotosController : Controller
  {
    private readonly UploadService uploadService;
    public FotosController(UploadService ul)
    {
      uploadService = ul;
    }

    [HttpPost("cargar")]
    public Object Cargar(IFormFile file)
    {
      try
      {
        if(!file.ContentType.StartsWith("image/")) {
          throw new Exception("No es una imagen");
        }
        
        var url_foto = this.uploadService.UploadFile(file);

        return new { error = false, data = url_foto };
      }
      catch (Exception err)
      {
        return new { error = true, data = err.Message };
      }
    }
  }
}
