using HomeTechRepair.Data;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class ManageDoctorApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageDoctorApiController(MyContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var docList = new List<ApplicationUser>();
            foreach(var user in _userManager.Users.ToList())
            {
                if(await _userManager.IsInRoleAsync(user,RoleModels.Doctor))
                {
                    docList.Add(user);
                }
            }
            return Ok(docList);
        }
    }
}
