using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class OperatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
