using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManageTicketApiController : ControllerBase
    {
        private readonly MyContext _dbContext;
        public ManageTicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Update([FromBody]CRUDModel<SupportTicketViewModel> value)
        {
            var st = value.value;
            var updatedTicket = _dbContext.SupportTickets.Include(x=>x.Appointment).Where(x => x.Id == st.Id).FirstOrDefault();
            if (updatedTicket.Appointment == null)
            {
                updatedTicket.Appointment = new Appointment()
                {
                    AppointmentDate = st.AppointmentDate,
                    SupportTicketId = st.Id,
                };
                _dbContext.Appointments.Add(updatedTicket.Appointment);
            }
            else
            {
                updatedTicket.Appointment.AppointmentDate = st.AppointmentDate;
            }
            updatedTicket.DoctorId = st.DoctorId;
            updatedTicket.ResolutionDate = st.ResolutionDate;

            _dbContext.SaveChanges();
            return Ok(value.value);
        }
    }
}
