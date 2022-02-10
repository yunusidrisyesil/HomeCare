using HomeTechRepair.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageTicketApiController : ControllerBase
    {
        private readonly MyContext _dbContext;

        public ManageTicketApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
