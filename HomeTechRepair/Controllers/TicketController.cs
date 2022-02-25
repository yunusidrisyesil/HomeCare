using AutoMapper;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
            try
            {
            var doctors = await _userManager.GetUsersInRoleAsync(RoleModels.Doctor);

            ViewBag.DataSource = doctors;
            return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Doctors could not uploaded, please try again.");
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
