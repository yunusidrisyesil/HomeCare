using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
            var count = _userManager.Users.Count() + 1;
            user = new ApplicationUser
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = count.ToString()
            };
          
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                //    protocol: Request.Scheme);
                //var emailMessage = new EmailMessage()
                //{
                //    Contacts = new string[] { user.Email },
                //    Body =
                //        $"User registered successfully",
                //    Subject = "...."
                //};


                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error has occurred.");
                return View(model);
            }
        }

        public IActionResult GetAllDoctors()
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

