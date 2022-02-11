using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly MyContext _dbContex;
        public DoctorController(MyContext dbContext)
        {
            _dbContex = dbContext;
        }
        public IActionResult GetAppoinment()
        {
            var data = _dbContex.Appointments.Include(x => x.SupportTicket).Where(x => x.SupportTicket.DoctorId== HttpContext.GetUserId()).ToList();
            ViewBag.DataSource = data;
            return View();
        }
    }
}
