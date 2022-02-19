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
    public class TicketApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        public TicketApiController(MyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            //Mapped not checked
            //var data = _dbContext.SupportTickets.Where(x=> x.UserId == HttpContext.GetUserId()).Select(x=> _mapper.Map<SupportTicketViewModel>(x)).ToList();
            var data = _dbContext.SupportTickets.Where(i => i.UserId == HttpContext.GetUserId()).Select(i => new SupportTicketViewModel
            {

                Id = i.Id,
                Description = i.Description,
                CreatedDate = i.CreatedDate

            }).ToList();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
    }
}
