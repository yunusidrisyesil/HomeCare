using AutoMapper;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.Services;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

    



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            bool isPassive = User.IsInRole(RoleModels.Passive);
            if (isPassive)
            {
                return RedirectToAction("ConfirmEmail", "Profile" );
            }
            var model = new UserProfileViewModel()
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,

            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(UserProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            user.Name = model.Name;
            user.Surname = model.Surname;
            if (user.Email != model.Email)
            {
                await _userManager.RemoveFromRoleAsync(user, RoleModels.User);
                await _userManager.AddToRoleAsync(user, RoleModels.Passive);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //TODO Email Confirmation
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Body = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here</a>",
                    Subject = "Email Confirmation"
                };
                await _emailSender.SendAsync(emailMessage);
            }
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, ModelState.ToFullErrorString());
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult UpdatePassword()
        {
            return View();

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                ViewBag.Message = "Password update successful";
                return RedirectToAction(nameof(Details));
            }
            else
            {
                ViewBag.Message = $"An error has occurred: {ModelState.ToFullErrorString()}";
            }

            return RedirectToAction(nameof(Profile));

        }



    }
}
