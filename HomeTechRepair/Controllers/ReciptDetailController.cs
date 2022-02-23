using HomeTechRepair.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HomeTechRepair.Controllers
{
    [Authorize]
    public class ReciptDetailController : Controller
    {

       [HttpGet]
        public IActionResult Index(Guid id)
        {
            return View();
        }
    }
}
