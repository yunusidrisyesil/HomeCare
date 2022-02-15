using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
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
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly MyContext _dbContext;

        public ManagePatientApiController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager, MyContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var user = _userManager.GetUsersInRoleAsync(RoleModels.User).Result;


            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
    }
}
