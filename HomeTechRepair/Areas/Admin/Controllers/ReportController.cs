using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
