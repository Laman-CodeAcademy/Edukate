using Microsoft.AspNetCore.Mvc;

namespace Edukate.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
