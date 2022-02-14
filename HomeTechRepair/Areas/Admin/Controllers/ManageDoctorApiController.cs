using DevExtreme.AspNet.Data;
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

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class ManageDoctorApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageDoctorApiController(MyContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var docList = new List<ApplicationUser>();
            foreach(var user in _userManager.Users.ToList())
            {
                if(await _userManager.IsInRoleAsync(user,RoleModels.Doctor))
                {
                    docList.Add(user);
                }
            }
            return Ok(DataSourceLoader.Load(docList,loadOptions));
        }

        [HttpPut]
        public async Task<IActionResult> Update(string key, string values)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == key);
            if(user == null)
            {
                return BadRequest();
            }

            JsonConvert.PopulateObject(values, user);
            if (!TryValidateModel(user))
            {
                return BadRequest();
            }

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest(); 
            }
            return Ok();
        }

        public async Task<IActionResult> DeleteAsync(string id)
        {
            //TODO
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
