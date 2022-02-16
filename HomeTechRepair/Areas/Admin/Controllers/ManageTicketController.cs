using HomeTechRepair.Data;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class ManageTicketController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageTicketController(MyContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {            
            var data = await _userManager.GetUsersInRoleAsync(RoleModels.Doctor);
            List<object> ddl = new List<object>();
            foreach (var item in data)
            {
                ddl.Add(new
                {
                    name = item.Name,
                    id = item.Id
                });
            }
            ViewBag.DropDownData = ddl;
            return View();
        }
       

    }
}
