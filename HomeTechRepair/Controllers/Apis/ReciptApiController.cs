using AutoMapper;
using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HomeTechRepair.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ReciptApiController : Controller
    {

        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;

        public ReciptApiController(MyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            //MAPPED not checked
            //var data = _dbContext.ReciptMasters.Where(x => x.UserId == HttpContext.GetUserId())
            //    .Select(x => _mapper.Map<ReciptViewModel>(x)).ToList();

            var data = _dbContext.ReciptMasters
                .Where(x => x.UserId == HttpContext.GetUserId())
                .Select(x => new ReciptViewModel
                {
                    Id = x.Id,
                    TotalAmount = x.TotalAmount,
                    Date = x.Date,
                    UserId = x.UserId,
                    isPaid= x.isPaid
                }).ToList();
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }



    }
}

