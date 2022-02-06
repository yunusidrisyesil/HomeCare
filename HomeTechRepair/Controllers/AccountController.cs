using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
         
        }

        //This method will be moved to admin controller
        //private void CheckAndAddRoles()
        //{
        //    foreach (var role in RoleModels.Roles)
        //    {
        //        if (!_roleManager.RoleExistsAsync(role).Result)
        //        {
        //           var result = _roleManager.CreateAsync(new ApplicationRole(role)).Result;
        //        }
        //    }
        //}


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Password), "Password is wrong");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isExist = await _userManager.FindByEmailAsync(model.Email);
            if (isExist == null)
            {
                var count = _userManager.Users.Count() + 1; // For username 
                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = count.ToString(),
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = "Passive";
                    if (count == 1)
                    {
                        role = "Admin";
                    }
                    await _userManager.AddToRoleAsync(user, role);
                    
                    //TODO Email confirmation

                    return RedirectToAction("Login", "Account");
                }

            }
            else
            {
                ModelState.AddModelError(nameof(model.Email), "This email is already in use");
                return View(model);
            }
            return View();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}