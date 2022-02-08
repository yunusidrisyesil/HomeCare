using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.Services;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    public class AccountController : Controller
    {
     
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public byte[] Encode { get; private set; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            CheckAndAddRoles();
            _emailSender = emailSender;
        }
        private void CheckAndAddRoles()
        {
            foreach (var role in RoleModels.Roles)
            {
                if (!_roleManager.RoleExistsAsync(role).Result)
                {
                    var result = _roleManager.CreateAsync(new ApplicationRole(role)).Result;
                }
            }
        }
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

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                    var email = new EmailMessage()
                    {
                        Contacts = new string[] { user.Email },
                        Subject = "Email Confirmation",
                        Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>",
                    };
                    await _emailSender.SendAsync(email);
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ViewBag.Message = "Not found E-mail";
            }
            else
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ConfirmResetPassword", "Account", new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Body =
                        $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>",
                    Subject = "Reset Password",
                };
                await _emailSender.SendAsync(emailMessage);
                ViewBag.Message = "Our password update instruction has been sent to your e-mail.";
            }

            return View();
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return BadRequest("Incorrect request");
            }

            ViewBag.Code = code;
            ViewBag.UserId = userId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View();
            }

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));

            var result = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);

            if (result.Succeeded)
            {

                TempData["Message"] = "Your password has been changed";
                return View();
            }
            else
            {
                var message = string.Join("<br>", result.Errors.Select(x => x.Description));
                TempData["Message"] = message;
                return View();
            }
        }
      
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]        
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);


            if(user == null || _userManager.IsInRoleAsync(user,RoleModels.User).Result)
            {
                return RedirectToAction("Index", "Home");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user,RoleModels.Passive);
                await _userManager.AddToRoleAsync(user,RoleModels.User);
            }
            return View();
        }
    }
}