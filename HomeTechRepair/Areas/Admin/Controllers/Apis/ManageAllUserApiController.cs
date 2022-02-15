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
            var user = _userManager.GetUsersInRoleAsync(RoleModels.User).Result.Select(x => new UserViewModel
            {
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Phone = x.PhoneNumber,
                Surname = x.Surname,
                RoleId = RoleModels.User
            });
            var doctor = _userManager.GetUsersInRoleAsync(RoleModels.Doctor).Result.Select(x => new UserViewModel
            {
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Phone = x.PhoneNumber,
                Surname = x.Surname,
                RoleId = RoleModels.Doctor
            });
            var oparetor = _userManager.GetUsersInRoleAsync(RoleModels.Operator).Result.Select(x => new UserViewModel
            {
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Phone = x.PhoneNumber,
                Surname = x.Surname,
                RoleId = RoleModels.Operator
            });
            var passive = _userManager.GetUsersInRoleAsync(RoleModels.Passive).Result.Select(x => new UserViewModel
            {
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Phone = x.PhoneNumber,
                Surname = x.Surname,
                RoleId = RoleModels.Passive
            });
            var admin = _userManager.GetUsersInRoleAsync(RoleModels.Admin).Result.Select(x => new UserViewModel
            {
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                Id = x.Id,
                Name = x.Name,
                Phone = x.PhoneNumber,
                Surname = x.Surname,
                RoleId = RoleModels.Admin
            });

            var allUser = new List<UserViewModel>();
            allUser.AddRange(user);
            allUser.AddRange(admin);
            allUser.AddRange(passive);
            allUser.AddRange(doctor);
            allUser.AddRange(passive);


            //var user = _userManager.Users;
            //var userroles = _dbContext.UserRoles;
            
            //var user2 = _dbContext.Users.Select(x => new UserViewModel()
            //{
            //    CreatedDate = x.CreatedDate,
            //    Email = x.Email,
            //    Id = x.Id,
            //    Name = x.Name,
            //    Phone = x.PhoneNumber,
            //    Surname = x.Surname,
            //}).ToList();

            return Ok(DataSourceLoader.Load(allUser, loadOptions));
        }
    }
}
