using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTickets()
        {
            var data = _dbContext.SupportTickets.Where(x => x.UserId == HttpContext.GetUserId());
            return Ok(data);
        }
    }
}
