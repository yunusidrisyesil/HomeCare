using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
  

        public ManageController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }

       
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeRegister()
        {

            var model = new EmployeeRegisterViewModel();
            model.Roles = new List<DropdownViewModel>();
            foreach (var role in RoleModels.Roles)
            {
                var data = await _roleManager.FindByNameAsync(role);
                model.Roles.Add(new DropdownViewModel
                {
                    Text = data.Name,
                    Value = data.Name
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeRegister(EmployeeRegisterViewModel model)
        {  
          
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Name);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.Name), "This user has already been registered.");
                return View(model);
            }

            user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                ModelState.AddModelError(nameof(model.Email), "This email has already been registered");
                return View(model);
            }
            string username = RandomGenerator.CreateUsername(5);
            user = new ApplicationUser
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = username
               
            };
             string password =RandomGenerator.CreatePassword(6);
            var result = await _userManager.CreateAsync(user,password);
         
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);

                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Body ="Your Password:"+ password,
                    Subject = "User Password"
                };
               await _emailSender.SendAsync(emailMessage);
                return RedirectToAction("Login", "Account",new { area = ""});
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error has occurred.");
                return View(model);
            }
        }

        public IActionResult Employees()
        {
            return View();
        }
        public IActionResult Patients()
        {
            return View();
        }
        public IActionResult Services() {
            return View();
        }
        public IActionResult AllUser()
        {
            ViewBag.Roles = _roleManager.Roles.Select(x => new
            {
                id = x.Id,
                name = x.Name
            }).ToList();
            return View();
        }

    }
}

