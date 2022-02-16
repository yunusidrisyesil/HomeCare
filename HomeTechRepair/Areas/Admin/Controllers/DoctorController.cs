using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public async Task<IActionResult> GetAppoinment(DataSourceLoadOptions loadOptions)
        {
            //var data = _dbContex.Appointments.Include(x => x.SupportTicket).Where(x => x.SupportTicket.DoctorId == HttpContext.GetUserId()).ToList();
            //ViewBag.DataSource = data;
            //return View();

            var appointments = _dbContex.Appointments.Include(x => x.SupportTicket).
               Where(x => x.SupportTicket.DoctorId == HttpContext.GetUserId()).Select(i => new
               {
                   Id = i.Id,
                   SupportTicketId = i.SupportTicketId,
                   AppointmentDate = i.AppointmentDate
               });

            return Json(await DataSourceLoader.LoadAsync(appointments, loadOptions));
        }

        [HttpGet]
        public IActionResult ConcludeTicket()
        {
            var ticketId = Guid.Parse("ecc5d1e8-d7ba-4426-a47f-08d9eb1b4856");
            var recipt = _dbContex.ReciptMasters.FirstOrDefault(x => x.SupportTicketId == ticketId);
            if (recipt == null)
            {
                var ticket = _dbContex.SupportTickets.Find(ticketId);
                recipt = new ReciptMaster
                {
                    SupportTicketId = ticketId,
                    UserId = ticket.UserId,
                    TotalAmount = 0,
                };
                _dbContex.ReciptMasters.Add(recipt);
                _dbContex.SaveChanges();
            }
            var model = new CloseTicketViewModel
            {
                ReciptMasterId = recipt.Id,
                SupportTicketId = recipt.SupportTicketId,
            };

            ViewBag.Data = _dbContex.Services.ToList();
            return View(model);
        }

        public IActionResult Scheduler()
        {
            ViewBag.Scheduler = DoctorsAppoitments();
            return View();
        }
        public IActionResult Agenda()
        {
            ViewBag.Agenda = DoctorsAppoitments();
            return View();
        }
        private List<AppointmentViewModel> DoctorsAppoitments()
        {
            var appoinmnetList = _dbContex.Appointments.Include(x => x.SupportTicket).Where(x => x.SupportTicket.DoctorId == HttpContext.GetUserId()).Select(x => new AppointmentViewModel
            {
                Id = x.Id,
                CreatedDate = x.SupportTicket.CreatedDate,
                AppointmentDate = x.AppointmentDate,
                StartDate = x.AppointmentDate.ToString("O"),
                EndDate = x.AppointmentDate.AddHours(1).ToString("O"),
                Description = x.SupportTicket.Description
            }).ToList();
            return appoinmnetList;
        }
    }
}
