using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Identiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("/api/[controller]/[action]")]
    public class ManageAllUserApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly MyContext _dbContext;

        public ManageAllUserApiController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, MyContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var user = new List<UserViewModel>();
            var roles = _roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                var userAdd = _userManager.GetUsersInRoleAsync(role.Name).Result.Select(x => new UserViewModel()
                {
                    CreatedDate = x.CreatedDate,
                    Email = x.Email,
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.PhoneNumber,
                    Surname = x.Surname,
                    RoleId = role.Id
                }).ToList();
                user.AddRange(userAdd);
            }
            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
        [HttpPut]
        public async Task<IActionResult> Update(string key, string values)
        {
            //TODO Role kontrol
            var user = _userManager.Users.FirstOrDefault(x => x.Id == key);
            if (user == null)
            {
                return BadRequest();
            }
            var userRole = _dbContext.UserRoles.FirstOrDefault(x => x.UserId == key);
            var userRoles = await _userManager.GetRolesAsync(user);
            var userView = new UserViewModel();
            userView.Email = user.Email;
            userView.CreatedDate = user.CreatedDate;
            userView.Id = user.Id;
            userView.Name = user.Name;
            userView.Phone = user.PhoneNumber;
            userView.Surname = user.Surname;
            userView.RoleId = userRole.RoleId;
            userView.UserRole = userRoles;


            //await _userManager.RemoveFromRoleAsync(user, oldrole.Name);

            JsonConvert.PopulateObject(values, userView );
            if (userRole.RoleId != userView.RoleId)
            {
                var newRole = _dbContext.Roles.FirstOrDefault(x=>x.Id == userView.RoleId);
                var oldRole = _dbContext.Roles.FirstOrDefault(x => x.Id == userRole.RoleId); ;
                await _userManager.RemoveFromRoleAsync(user, oldRole.Name);
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }
            //gelen roleidlere göre yeni rol bilgisi eski rol bilgisi ile kontrol edilecek

            user.Email =userView.Email;
            user.CreatedDate= userView.CreatedDate;
            user.Id= userView.Id;
            user.Name= userView.Name;
            user.PhoneNumber= userView.Phone;
            user.Surname = userView.Surname ;
            
            //user update işlemleri

            if (!TryValidateModel(user))
            {
                return BadRequest();
            }
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
        //[HttpDelete]
        //public async Task<IActionResult> DeleteAsync(string id)
        //{

        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user != null)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
