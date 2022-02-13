using DevExtreme.AspNet.Data;
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
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
      
            var  appointments = _dbContex.Appointments.Include(x => x.SupportTicket).
               Where(x => x.SupportTicket.DoctorId == HttpContext.GetUserId()).Select(i => new {
              Id = i.Id,
              SupportTicketId =  i.SupportTicketId,
              AppointmentDate = i.AppointmentDate
            });

            return Json(await DataSourceLoader.LoadAsync(appointments, loadOptions));
        }
        [HttpPost]
        public IActionResult Post(string values)
        {
            var newAppointment = new Appointment();
            JsonConvert.PopulateObject(values, newAppointment);

            if (!TryValidateModel(newAppointment))
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });

            _dbContex.Appointments.Add(newAppointment);
            _dbContex.SaveChanges();

            return Ok();
        }

       
    }
}
