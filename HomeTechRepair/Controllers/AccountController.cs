using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Controllers
{
	public class AccountController : Controller
	{

		private readonly UserManager<ApplicationUser> _userManager;


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
		public IActionResult Register(RegisterViewModel model)
		{
            if (!ModelState.IsValid)
            {
				return View(model);
            }

			var result = _userManager.Users;

			return View();
		}
	}
}
