using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
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

        public ManageAllUserApiController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                    Role = role.Id
                }).ToList();
                user.AddRange(userAdd);
            }
            return Ok(DataSourceLoader.Load(user, loadOptions));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(string key, string values)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == key);
            if (user == null)
            {
                return BadRequest();
            }
            JsonConvert.PopulateObject(values, user);
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
