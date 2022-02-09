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

namespace HomeTechRepair.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _dbContext;
        public TicketController(UserManager<ApplicationUser> userManager, MyContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            if (user != null)
            {
                try
                {
                    _dbContext.SupportTickets.Add(new SupportTicket
                    {
                        Description = model.Description,
                        UserId = user.Id,
                    });
                    _dbContext.SaveChanges();
                    ViewBag.Message = "Support ticket created succesfully";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(model);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult GetTickets()
        {
            //TODO Get Doctor Name
            var data = _dbContext.SupportTickets.ToList().Where(x => x.UserId == HttpContext.GetUserId()).ToList();
            ViewBag.DataSource = data;
            return View();
        }
    }
}
