using AutoMapper;
using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class TicketApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketApiController(MyContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
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

        [HttpPost]
        public async Task<IActionResult> Insert(TicketViewModel data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
                if (user != null)
                {
                    var address = _dbContext.Addresses.FirstOrDefault(x => x.UserId == user.Id);
                    if (address == null)
                    {
                        address = new Address
                        {
                            UserId = HttpContext.GetUserId(),
                            StateId = 0,
                        };
                        _dbContext.Addresses.Add(address);
                    }

                    _dbContext.SupportTickets.Add(new SupportTicket
                    {
                        Description = data.Description,
                        UserId = user.Id,
                    });

                    address.Latitude = Convert.ToDouble(data.Latitude);
                    address.Longitude = Convert.ToDouble(data.Longitude);
                    _dbContext.SaveChanges();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
