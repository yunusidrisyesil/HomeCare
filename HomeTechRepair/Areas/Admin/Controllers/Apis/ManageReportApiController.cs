using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ManageReportApiController : Controller
    {
        private readonly MyContext _dbContext;

        public ManageReportApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.ReciptMasters.Select(x => new ReciptViewModel
            {
                Id = x.Id,
                TotalAmount = x.TotalAmount,
                Date = x.Date
            }).ToList();
            if (data == null)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

    }
}
