using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;

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
            try
            {
                var data = _dbContex.Services.Select(i => new ServiceViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price
                }).ToList();
                return Ok(DataSourceLoader.Load(data, loadOptions));
            }
            catch (Exception)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            }

        }

        [HttpPut]
        public IActionResult Update(Guid key, string values)
        {
            var data = _dbContex.Services.Find(key);
            if (data == null)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            }
            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
            {
                return BadRequest(ModelState.ToFullErrorString());
            }
            var result = _dbContex.SaveChanges();
            if (result == 0)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Service could not update."
                });
            }
            return Ok(new JsonResponseViewModel());
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var data = new Service();
            JsonConvert.PopulateObject(values, data);
            if (!TryValidateModel(data))
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()
                });
            }
            _dbContex.Services.Add(data);
            var result = _dbContex.SaveChanges();
            if (result == 0)
            {
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "New service could not added."
                });
            }
            return Ok(new JsonResponseViewModel());
        }

        [HttpDelete]
        public IActionResult Delete(Guid key)
        {
            var data = _dbContex.Services.Find(key);
            if (data == null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Service not found.");
            }
            _dbContex.Services.Remove(data);
            var result = _dbContex.SaveChanges();
            if (result == 0)
            {
                return BadRequest("An error occured.Try again later.");
            }
            return Ok(new JsonResponseViewModel());
        }
    }
}

