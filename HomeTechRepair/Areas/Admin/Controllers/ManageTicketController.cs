using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                    text = item.Name,
                    id = item.Id
                });
            }
            ViewBag.DropDownData = ddl;
            return View();
        }
       

    }
}
