using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("/api/[controller]/[action]")]
    public class ManageAllUserApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _dbContext;

        public ManageAllUserApiController(UserManager<ApplicationUser> userManager, MyContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            //var user = _userManager.Users;
            var userroles = _dbContext.UserRoles;
            
            var user = _dbContext.Users.Select(x => new UserViewModel()
            {
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Phone = x.PhoneNumber,
                Surname = x.Surname
            }).ToList();
            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
    }
}
