using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MatchAppUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult MainWindow()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}