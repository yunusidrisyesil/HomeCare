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
using System.Linq;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ManageTicketApiController : Controller
    {
        private readonly MyContext _dbContext;
        public ManageTicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.SupportTickets.Include(x => x.Appointment).Include(x => x.User).Select(x => new SupportTicketViewModel
            {
                Id = x.Id,
                Patient = x.User.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                AppointmentDate = x.Appointment.AppointmentDate,
                ResolutionDate = x.ResolutionDate,
                DoctorId = x.DoctorId,
                isActive = (x.ResolutionDate != null) ? true : false
            }).ToArray();
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
        [HttpPut]
        public IActionResult Update(Guid key, string values)
        {
            bool delete = false;
            var data = _dbContext.SupportTickets.Include(x => x.Appointment).Where(x => x.Id == key).FirstOrDefault();
            if (data == null)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            if(data.OperatorId==null)
            {
                data.OperatorId = HttpContext.GetUserId();
            }
            JsonConvert.PopulateObject(values, data);
            if (data.Appointment == null)
            {

                var appointment = new Appointment()
                {
                    SupportTicketId = data.Id,
                    AppointmentDate = DateTime.Now
                };
                delete = true;
                _dbContext.Appointments.Add(appointment);
                _dbContext.SaveChanges();
            }
            JsonConvert.PopulateObject(values, data.Appointment);
            var date = _dbContext.SupportTickets.Include(x => x.Appointment).Where(x => x.DoctorId == data.DoctorId)
                .Where(x => x.Appointment.AppointmentDate.Date == data.Appointment.AppointmentDate.Date).ToList();
            if (date.Count != 0)
            {
                foreach (var item in date)
                {
                    var eda = _dbContext.Appointments.FirstOrDefault(x => x.SupportTicketId == item.Id);
                    if (eda.SupportTicketId == data.Id)
                    {
                        continue;
                    }
                    if (eda.AppointmentDate <= data.Appointment.AppointmentDate && data.Appointment.AppointmentDate <= eda.AppointmentDate.AddHours(1))
                    {
                        if (delete)
                        {
                            _dbContext.Appointments.Remove(data.Appointment);
                            _dbContext.SaveChanges();
                        }
                        return BadRequest();
                    }
                }
            }

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
