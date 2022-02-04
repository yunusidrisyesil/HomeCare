using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
