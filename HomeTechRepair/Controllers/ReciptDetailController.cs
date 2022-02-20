using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}
