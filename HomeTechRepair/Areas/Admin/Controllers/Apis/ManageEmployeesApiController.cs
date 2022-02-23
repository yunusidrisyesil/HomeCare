using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("/api/[controller]/[action]")]
    public class ManageEmployeesApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageEmployeesApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                var employeesList = new List<ApplicationUser>();
                var docList = await _userManager.GetUsersInRoleAsync(RoleModels.Doctor);
                var operatorList = await _userManager.GetUsersInRoleAsync(RoleModels.Operator);
                employeesList.AddRange(docList);
                employeesList.AddRange(operatorList);
                return Ok(DataSourceLoader.Load(employeesList, loadOptions));
            }
            catch (Exception)
            {
                return BadRequest(new JsonResponseViewModel
                {
                    IsSuccess = false,
                    ErrorMessage = "Data cannot found."
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(string key, string values)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == key);
            if (user == null)
                return StatusCode(StatusCodes.Status409Conflict, new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "User cannot found."
                });

            JsonConvert.PopulateObject(values, user);
            if (!TryValidateModel(user))
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "User cannot updated."
                });
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "User cannot updated."
                });
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string key)
        {
            //TODO
            var user = await _userManager.FindByIdAsync(key);
            if (user != null)
            {
                await _userManager.SetEmailAsync(user,user.Name+user.Surname+DateTime.UtcNow.ToString()+"@deleted.com");
                return Ok();
            }
            else
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Delete operation cannot helded."
                });
            }
        }
    }
}
