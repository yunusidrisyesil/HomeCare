using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class CloseTicketApiController : Controller
    {
        private readonly MyContext _dbContext;

        public CloseTicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(Guid id, DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.ReciptDetails.Include(x => x.Service).Where(x => x.ReciptMasterId == id).Select(x => new Service
            {
                Id = x.Service.Id,
                Name = x.Service.Name,
                Price = x.Service.Price
            });
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

        public IActionResult Insert()
        {
            return Ok();
        }
    }
}
