using HomeTechRepair.Areas.Admin.ViewModels;
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
        //model.EmployeesList= data.Select(x => new Itemlist { Value = x.EmployeeId, Text = x.EmployeeName }).ToList();

    [HttpGet]
        public IActionResult RolesRegister()
        {
            return View();
        }
    
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
