using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HomeTechRepair.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TicketApiController : Controller
    {

        private readonly MyContext _dbContext;

        public TicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            var data = _dbContext.SupportTickets.Where(i => i.UserId ==HttpContext.GetUserId()).Select(i => new SupportTicketViewModel
            {

                Id = i.Id,
                Description = i.Description,
                CreatedDate = i.CreatedDate

            }).ToList();

            return Ok(DataSourceLoader.Load(data, loadOptions));
        }
        public IActionResult Index()
        {
            return View();
        }



    }
}
