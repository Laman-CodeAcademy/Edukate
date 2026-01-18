using Microsoft.AspNetCore.Mvc;

namespace Edukate.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
