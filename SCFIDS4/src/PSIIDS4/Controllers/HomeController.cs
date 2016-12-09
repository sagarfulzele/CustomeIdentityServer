using Microsoft.AspNetCore.Mvc;

namespace PSIIDS4.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}