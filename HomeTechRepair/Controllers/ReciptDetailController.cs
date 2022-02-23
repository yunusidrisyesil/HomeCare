using HomeTechRepair.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeTechRepair.Controllers
{
    public class ReciptDetailController : Controller
    {
        private readonly MyContext _dbContext;

        public ReciptDetailController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

       [HttpGet]
        public IActionResult Index(Guid id)
        {
            return View();
        }
    }
}
