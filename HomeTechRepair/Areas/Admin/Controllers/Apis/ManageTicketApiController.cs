using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.Services;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ManageTicketApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ManageTicketApiController(MyContext dbContext, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _emailSender = emailSender;
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
        public async Task<IActionResult> Update(Guid key, string values)
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
                var doctor = await _userManager.FindByIdAsync(data.DoctorId);

                var callbackUrl = Url.Action("Scheduler", "Doctor", new { Area = "Admin" } , protocol: Request.Scheme);

                var email = new EmailMessage()
                {
                    Contacts = new string[] { doctor.Email },
                    Subject = "New Appointment",
                    Body = $"New appoinment has been created.Please check your Scheduler by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>",
                };
                await _emailSender.SendAsync(email);
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
