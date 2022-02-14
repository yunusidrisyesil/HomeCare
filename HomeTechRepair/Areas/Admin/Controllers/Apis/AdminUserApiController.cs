using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class AdminUserApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _dbContext;

        public AdminUserApiController(UserManager<ApplicationUser> userManager, MyContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var user = _dbContext.Users.ToList();

            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
    }
}
