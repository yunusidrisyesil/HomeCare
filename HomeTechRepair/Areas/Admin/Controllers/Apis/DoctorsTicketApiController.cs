using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    public class DoctorsTicketApiController : Controller
    {
        private readonly MyContext _dbContext;

        public DoctorsTicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public IActionResult Get()
        //{
        //    var data = _dbContext.SupportTickets.Include(x => x.Appointment).Where(x=>x.DoctorId==HttpContext.GetUserId()).Select(x => new SupportTicketViewModel
        //    {
        //        Id = x.Id,
        //        Patient = x.User.Name,
        //        Description = x.Description,
        //        CreatedDate = x.CreatedDate,
        //        AppointmentDate = x.Appointment.AppointmentDate,
        //        ResolutionDate = x.ResolutionDate
        //    }).ToArray();
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}
