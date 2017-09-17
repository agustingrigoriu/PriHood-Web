using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.Net.Http.Headers;

namespace PriHood.Auth
{
  public class UploadService
  {

    private IHostingEnvironment _hostingEnvironment;
    public UploadService(HostingEnvironment environment)
    {
      _hostingEnvironment = environment;
    }

    [HttpPost]
    public Object UploadFiles(IList<IFormFile> files, string folder)
    {
      var filename = "";
      long size = 0;
      foreach (var file in files)
      {
        filename = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition)
                        .FileName
                        .Trim('"');
        filename = _hostingEnvironment.WebRootPath + folder;
        size += file.Length;
        using (FileStream fs = System.IO.File.Create(filename))
        {
          file.CopyTo(fs);
          fs.Flush();
        }
      }

      return new { filename = filename };
    }

  }
}