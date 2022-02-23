using AutoMapper;
using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class TicketApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketApiController(MyContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            //Mapped not checked
            //var data = _dbContext.SupportTickets.Where(x=> x.UserId == HttpContext.GetUserId()).Select(x=> _mapper.Map<SupportTicketViewModel>(x)).ToList();
            var data = _dbContext.SupportTickets.Include(x=>x.Appointment).Where(i => i.UserId == HttpContext.GetUserId()).Select(i => new SupportTicketViewModel
            {

                Id = i.Id,
                Description = i.Description,
                CreatedDate = i.CreatedDate,
                AppointmentDate = i.Appointment.AppointmentDate,
                DoctorId=i.DoctorId,
                ResolutionDate = i.ResolutionDate,
                Patient = i.UserId               
            }).ToList();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(TicketViewModel data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
                if (user != null)
                {
                    _dbContext.SupportTickets.Add(new SupportTicket
                    {
                        Description = data.Description,
                        UserId = user.Id,
                        Longitude = Convert.ToDouble(data.Longitude),
                        Latitude = Convert.ToDouble(data.Latitude)
                    });
                    _dbContext.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
