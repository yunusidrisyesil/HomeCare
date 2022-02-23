using HomeTechRepair.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Controllers
{
    public class ReciptController : Controller
    {
        private readonly MyContext _dbContext;

        public ReciptController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

       [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
