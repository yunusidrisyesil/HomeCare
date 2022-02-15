using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{

    [Route("api/[controller]/[action]")]
    public class ManageServiceApiController : Controller
    {
        private readonly MyContext _dbContex;

        public ManageServiceApiController(MyContext dbContext)
        {
            _dbContex = dbContext;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {

            var data = _dbContex.Services.Select(i => new ServiceViewModel
              {
                  Id = i.Id,
                  Name = i.Name,
                  Price = i.Price
              }).ToArray();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
        [HttpPut]
        public IActionResult Update(Guid key, string values)
        {
            var data = _dbContex.Services.Include(x => x.Id == key).FirstOrDefault();
            if (data == null)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            if (data.Id == null)
            {
                var service = new  Service()
                {
                   Id = data.Id,
                   Name = data.Name,
                   Price = data.Price
                };
                _dbContex.Services.Add(service);
                _dbContex.SaveChanges();
               
            }
            JsonConvert.PopulateObject(values, data);
            JsonConvert.PopulateObject(values, data.Id);
            if (!TryValidateModel(data))
                return BadRequest(ModelState.ToFullErrorString());
            var result = _dbContex.SaveChanges();
            if (result == 0)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Service could not edited."
                });
            return Ok(new JsonResponseViewModel());
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {

            var data = new Service();
            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            _dbContex.Services.Add(data);

            var result = _dbContex.SaveChanges();
            if (result == 0)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "New service could not added."
                });
            return Ok(new JsonResponseViewModel());

        }



    }

}

