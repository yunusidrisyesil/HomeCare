using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var roles = new List<LookUpViewModel>();
            var user = new List<UserViewModel>();
            

            foreach (var role in RoleModels.Roles)
            {
                var userAdd = _userManager.GetUsersInRoleAsync(role).Result.Select(x => new UserViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    Email = x.Email,
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.PhoneNumber,
                    Surname = x.Surname,
                    RoleName = role
                }).ToList();
                user.AddRange(userAdd);
                roles.Add(new LookUpViewModel 
                {
                    Id = role,
                    Name = role
                });
            }
            ViewBag.Roles = roles;
            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
    }
}
