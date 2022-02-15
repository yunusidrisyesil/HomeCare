using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageServiceApiController : Controller
    {
        private readonly MyContext _dbContex;
      
        public ManageServiceApiController(MyContext dbContext)
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
    


    }
}
