using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.IO.Site.Controllers
{
    public class CopaFilmesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}