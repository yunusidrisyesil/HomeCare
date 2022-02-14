using DevExtreme.AspNet.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("/api/[controller]/[action]")]
    public class ManagePatientApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManagePatientApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var user = _userManager.GetUsersInRoleAsync(RoleModels.User).Result;

            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
    }
}
