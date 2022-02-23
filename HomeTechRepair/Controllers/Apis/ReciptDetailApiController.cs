using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeTechRepair.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ReciptDetailApiController : Controller
    {

        private readonly MyContext _dbContext;

        public ReciptDetailApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
            var model = _dbContext.ReciptDetails.Include(x => x.ReciptMaster).Include(x => x.Service).Select(x => new ReciptViewModel
                {
                  Id = x.ReciptMasterId,
                  ServiceId = x.ServiceId,
                  Name = x.Service.Name,
                  ServicePrice = x.ServicePrice,
                  Quantity = x.Quantity,
                  Description = x.Description
                }).ToList();
                return Ok(DataSourceLoader.Load(model, loadOptions));
            }
            catch (System.Exception)
            {
                return BadRequest()
              
            }
        }

    }
}

