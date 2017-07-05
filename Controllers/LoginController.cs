using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PriHood.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar() {
          string[] Perfiles = { "Administrador", "Residente", "Encagado de Seguridad"};
          ViewData["Perfiles"] = Perfiles;
          return View();
        }
    }
}
