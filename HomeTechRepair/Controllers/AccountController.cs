using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
	public class AccountController : Controller
	{

		private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
		public IActionResult Login()
		{
			return View();
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
			if(isExist == null)
            {
				var count = _userManager.Users.Count(); // For username 
				var user = new ApplicationUser{
					Name = model.Name,
					Surname = model.Surname,
					Email = model.Email,
					UserName = (count+1).ToString(),
                };

				var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
					//TODO Email confirmation and user role 

					return RedirectToAction("Login","Account");
                }

            }
            else
            {
				ModelState.AddModelError(nameof(model.Email),"This email is already in use");
				return View(model);
            }
			return View();
		}
	}
}
