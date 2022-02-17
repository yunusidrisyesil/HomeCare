using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ReciptApiController : Controller
    {
       
        private readonly MyContext _dbContext;

        public ReciptApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.ReciptMasters.Where(x => x.UserId == HttpContext.GetUserId()).Select(x => new ReciptViewModel
            {

                Id = x.Id,
                TotalAmount = x.TotalAmount,
                Date = x.Date,
                UserId = x.UserId
            }).ToList();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

    }
}
