using DevExtreme.AspNet.Data;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            }).ToList();
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

        [HttpPut]
        public IActionResult Update(string key, string values, string extraParam)
        {
            var reciptId = Guid.Parse(extraParam);
            //var ReciptDetials = _dbContext.ReciptDetails.Select(x => x.Description);
            Service service = new Service();
            JsonConvert.PopulateObject(values, service);
            return Ok();
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            return Ok();
        }

        public IActionResult GetPrice(Guid id)
        {
            var price = _dbContext.Services.FirstOrDefault(x => x.Id == id);
            return Ok(price.Price);
        }
    }
}
