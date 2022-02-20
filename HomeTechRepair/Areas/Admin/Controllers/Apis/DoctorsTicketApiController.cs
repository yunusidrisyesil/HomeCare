using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class DoctorsTicketApiController : Controller
    {
        private readonly MyContext _dbContext;

        public DoctorsTicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.SupportTickets.Include(x => x.Appointment).Where(x => x.DoctorId == HttpContext.GetUserId()).Select(x => new SupportTicketViewModel
            {
                Id = x.Id,
                Patient = x.User.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                AppointmentDate = x.Appointment.AppointmentDate,
                ResolutionDate = x.ResolutionDate,
                isActive = (x.ResolutionDate != null) ? true : false
            }).ToList();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
    }
}
