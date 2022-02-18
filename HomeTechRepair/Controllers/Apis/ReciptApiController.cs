using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var data = _dbContext.ReciptMasters
                .Where(x => x.UserId == "c2b8f513-32ed-470b-aa9e-57788439b6aa")
                .Select(x => new ReciptViewModel
                {

                    ReciptMasterId = x.Id,
                    TotalAmount = x.TotalAmount,
                    Date = x.Date,
                    UserId = x.UserId,
                    hasitems = _dbContext.ReciptDetails.Count(b => b.ReciptMasterId == x.Id) > 0
                }).ToList();
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

        [HttpGet]
        public IActionResult GetReciptDetail(DataSourceLoadOptions loadOptions)
        {
            var datard = _dbContext.ReciptDetails.Include(x => x.ReciptMaster)
              .Include(x => x.Service).Select(x => new ReciptViewModel
              {
                  ReciptMasterId = x.ReciptMasterId,
                  Id = x.ServiceId,
                  Name = x.Service.Name,
                  ServicePrice = x.ServicePrice,
                  Quantity = x.Quantity,
                  Description = x.Description
              }).ToList();


            return Ok(DataSourceLoader.Load(datard, loadOptions));

        }

    }
}

