using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Controllers
{
    [Authorize]
    public class ReciptController : Controller
    {
       [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
