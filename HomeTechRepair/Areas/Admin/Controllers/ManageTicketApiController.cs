using System;
using System.Linq;
using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        //    public IActionResult Update()
        //    {
        //        var updatedTicket = _dbContext.SupportTickets.Include(x => x.Appointment).Where(x => x.Id == st.Id).FirstOrDefault();
        //        if (updatedTicket.Appointment == null)
        //        {
        //            updatedTicket.Appointment = new Appointment()
        //            {
        //                AppointmentDate = st.AppointmentDate,
        //                SupportTicketId = st.Id,
        //            };
        //            _dbContext.Appointments.Add(updatedTicket.Appointment);
        //        }
        //        else
        //        {
        //            updatedTicket.Appointment.AppointmentDate = st.AppointmentDate;
        //        }
        //        updatedTicket.DoctorId = st.DoctorId;
        //        updatedTicket.ResolutionDate = st.ResolutionDate;

        //        _dbContext.SaveChanges();
        //        return Ok();

        //    }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.SupportTickets.Include(x => x.Appointment).Include(x=>x.User).Select(x => new SupportTicketViewModel
            {
                Id = x.Id,
                Patient=x.User.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                AppointmentDate = x.Appointment.AppointmentDate,
                ResolutionDate = x.ResolutionDate,
                //DoctorId = x.DoctorId
            }).ToArray();
            return Ok(DataSourceLoader.Load(data,loadOptions));
        }
        [HttpPut]
        public IActionResult Update(Guid key,string values)
        {
            var data = _dbContext.SupportTickets.Find(key);
            if (data == null)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
                return BadRequest(ModelState.ToFullErrorString());
            var result = _dbContext.SaveChanges();
            if (result == 0)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Support ticket could not edited."
                });
            return Ok(new JsonResponseViewModel());
        }
        [HttpPost]
        public IActionResult Insert(string values)
        {
            var data = new SupportTicket();
            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            _dbContext.SupportTickets.Add(data);

            var result = _dbContext.SaveChanges();
            if (result == 0)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "New support ticket could not added."
                });
            return Ok(new JsonResponseViewModel());
        }
    }
}
