using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
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
            var data = _dbContext.ReciptDetails.Include(x => x.Service).Where(x => x.ReciptMasterId == id).Select(x => new ReciptServiceViewModel
            {
                Id = x.Service.Id,
                Name = x.Service.Name,
                ReciptPrice = x.Service.Price,
                Description = x.Description,
                Quantity = x.Quantity
            }).ToList();
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

        [HttpPut]
        public IActionResult Update(string key, string values, string extraParam)
        {
            var service = new ReciptServiceViewModel();
            JsonConvert.PopulateObject(values, service);
            var reciptDetials = _dbContext.ReciptDetails.Where(x => x.ReciptMasterId == Guid.Parse(extraParam)).Where(x => x.ServiceId == Guid.Parse(key)).ToList().First();
            var doesExists = _dbContext.ReciptDetails.Where(x => x.ReciptMasterId == Guid.Parse(extraParam)).Where(x => x.ServiceId == service.Id).ToList();
            if (doesExists.Count != 0)
            {
                doesExists.First().Quantity += service.Quantity;
                if (service.Price != 0)
                {
                    doesExists.First().ServicePrice = service.Price;
                }
                doesExists.First().Description = doesExists.First().Description + " / " + service.Description;
                _dbContext.ReciptDetails.Remove(reciptDetials);
            }
            else
            {
                _dbContext.Remove(reciptDetials);
                _dbContext.SaveChanges();
                if (service.Id != Guid.Empty)
                {
                    reciptDetials.ServiceId = service.Id;
                }
                if (service.Price != 0)
                {
                    reciptDetials.ServicePrice = service.Price;
                }
                reciptDetials.Quantity = service.Quantity;
                reciptDetials.Description = service.Description;
                _dbContext.Add(reciptDetials);
            }
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Insert(string values, string extraParam)
        {
            var recipt = new ReciptDetail();
            var service = new ReciptServiceViewModel();
            JsonConvert.PopulateObject(values, service);
            var doesExists = _dbContext.ReciptDetails.Where(x => x.ReciptMasterId == Guid.Parse(extraParam))
                                                     .Where(x => x.ServiceId == service.Id).ToList();
            if (doesExists.Count != 0)
            {
                doesExists.First().Quantity += service.Quantity;
                doesExists.First().Description = doesExists.First().Description + " / " + service.Description;
                doesExists.First().ServicePrice = service.Price;
            }
            else
            {
                recipt.ServiceId = service.Id;
                recipt.ReciptMasterId = Guid.Parse(extraParam);
                recipt.Quantity = service.Quantity;
                recipt.ServicePrice = service.Price;
                _dbContext.ReciptDetails.Add(recipt);
            }
            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(string key, string extraParam)
        {
            var reciptService = _dbContext.ReciptDetails
                                        .Where(x => x.ReciptMasterId == Guid.Parse(extraParam))
                                        .Where(x => x.ServiceId == Guid.Parse(key)).ToList().First();
            _dbContext.Remove(reciptService);
            _dbContext.SaveChanges();
            return Ok();
        }

        public IActionResult GetPrice(Guid id)
        {
            var price = _dbContext.Services.FirstOrDefault(x => x.Id == id);
            return Ok(price.Price);
        }
    }
}
