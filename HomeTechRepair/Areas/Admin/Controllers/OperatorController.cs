using HomeTechRepair.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    public class OperatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
