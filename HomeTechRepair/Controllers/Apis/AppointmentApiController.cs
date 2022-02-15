using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeTechRepair.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class AppointmentApiController : Controller
    {
        private readonly MyContext _dbContext;

        public AppointmentApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var appoinmnetList = _dbContext.Appointments.Include(x => x.SupportTicket).Where(x => x.SupportTicket.UserId == HttpContext.GetUserId()).Select(x => new AppointmentViewModel
            {
                Id = x.Id,
                CreatedDate = x.SupportTicket.CreatedDate,
                AppointmentDate = x.AppointmentDate,
                Description = x.SupportTicket.Description,
                ResolutionDate = x.SupportTicket.ResolutionDate,
                DoctorId = x.SupportTicket.DoctorId,
                isActive = (x.SupportTicket.ResolutionDate != null) ? true : false
            }).ToList(); ;

            return Ok(DataSourceLoader.Load(appoinmnetList, loadOptions));
        }
    }
}
