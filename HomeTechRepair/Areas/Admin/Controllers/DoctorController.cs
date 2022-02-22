using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,Doctor")]
    public class DoctorController : Controller
    {
        private readonly MyContext _dbContex;

        public DoctorController(MyContext dbContext)
        {
            _dbContex = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAppointment(DataSourceLoadOptions loadOptions)
        {

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
        [Authorize]
        public IActionResult ConcludeTicket(Guid Id)
        {
            //var ticketId = Guid.Parse("ecc5d1e8-d7ba-4426-a47f-08d9eb1b4856");
            var recipt = _dbContex.ReciptMasters.FirstOrDefault(x => x.SupportTicketId == Id);
            if (recipt == null)
            {
                var ticket = _dbContex.SupportTickets.Find(Id);
                recipt = new ReciptMaster
                {
                    SupportTicketId = Id,
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
            ViewBag.Data = _dbContex.Services.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Scheduler()
        {
            ViewBag.Scheduler = DoctorsAppoitments();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Agenda()
        {
            ViewBag.Agenda = DoctorsAppoitments();
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Tickets()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
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

        [HttpGet]
        [Authorize]
        public IActionResult GetTicketLocationDetails(Guid id)
        {
            var ticket = _dbContex.SupportTickets.FirstOrDefault(x => x.Id == id);
            if (ticket == null)
            {
                return RedirectToAction("Tickets", "Doctor", new { area = "Admin" });
            }
            return View(ticket);
        }
    }
}
