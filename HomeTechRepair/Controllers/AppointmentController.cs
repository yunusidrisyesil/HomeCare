using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> GetAppointments()
        {
            ViewBag.Doctors = await _userManager.GetUsersInRoleAsync(RoleModels.Doctor);
            return View();
        }
    }
}
