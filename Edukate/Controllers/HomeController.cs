using System.Diagnostics;
using Edukate.Models;
using Microsoft.AspNetCore.Mvc;

namespace Edukate.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
