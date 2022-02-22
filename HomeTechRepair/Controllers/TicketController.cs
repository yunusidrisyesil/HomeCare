using AutoMapper;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        public TicketController(UserManager<ApplicationUser> userManager, MyContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult CreateTicket()
        {
            return View();
        }
        public async Task<IActionResult> GetTickets()
        {
            var ticketList = _dbContext.SupportTickets.Where(x => x.UserId == HttpContext.GetUserId());
            List<UserTicketViewModel> model = new List<UserTicketViewModel>();
            foreach (var item in ticketList)
            {
                var data = _mapper.Map<UserTicketViewModel>(item);
                if (item.DoctorId != null)
                {
                    var doctor = await _userManager.FindByIdAsync(item.DoctorId);
                    data.DoctorName = doctor.Name;
                }
                model.Add(data);
            }
            ViewBag.DataSource = model;
            return View();
        }
    }
}
