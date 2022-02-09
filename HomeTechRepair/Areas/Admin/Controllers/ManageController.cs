using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        //private readonly RoleManager<ApplicationRole> _roleManager;

        public ManageController(RoleManager<ApplicationRole> roleManager)
        {
            //_roleManager = roleManager;
            //CheckAndAddRoles();
        }

        //private void CheckAndAddRoles()
        //{
        //    foreach (var role in RoleModels.Roles)
        //    {
        //        if (!_roleManager.RoleExistsAsync(role).Result)
        //        {
        //            var result = _roleManager.CreateAsync(new ApplicationRole(role)).Result;
        //        }
        //    }
        //}


        //[HttpGet]
        //public IActionResult RolesRegister()
        //{
        //    return View();
        //}

        //method will be posted



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }


    }
}
