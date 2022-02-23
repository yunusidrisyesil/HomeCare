using DevExtreme.AspNet.Data;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class CloseTicketApiController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public CloseTicketApiController(MyContext dbContext, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _emailSender = emailSender;
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
            var reciptMaster = _dbContext.ReciptMasters.Find(Guid.Parse(extraParam));
            var reciptDetials = _dbContext.ReciptDetails.Where(x => x.ReciptMasterId == Guid.Parse(extraParam)).Where(x => x.ServiceId == Guid.Parse(key)).ToList().First();
            var difference = service.Quantity - reciptDetials.Quantity;
            reciptMaster.TotalAmount += reciptDetials.ServicePrice * difference;

            _dbContext.Remove(reciptDetials);
            _dbContext.SaveChanges();

            if (service.Id != Guid.Empty)
            {
                reciptDetials.ServiceId = service.Id;
            }

            reciptDetials.Quantity = service.Quantity;

            if (service.Price != 0)
            {
                reciptDetials.ServicePrice = service.Price;
                reciptMaster.TotalAmount += service.Price * service.Quantity;
            }
            reciptDetials.Description = service.Description;
            _dbContext.Add(reciptDetials);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult Insert(string values, string extraParam)
        {
            var recipt = new ReciptDetail();
            var service = new ReciptServiceViewModel();
            var reciptMaster = _dbContext.ReciptMasters.Find(Guid.Parse(extraParam));
            JsonConvert.PopulateObject(values, service);
            var doesExists = _dbContext.ReciptDetails.Where(x => x.ReciptMasterId == Guid.Parse(extraParam))
                                                     .Where(x => x.ServiceId == service.Id).ToList();
            if (doesExists.Count != 0)
            {
                doesExists.First().Quantity += service.Quantity;
                doesExists.First().Description = doesExists.First().Description + " / " + service.Description;
                doesExists.First().ServicePrice = service.Price;
                reciptMaster.TotalAmount += service.Price * service.Quantity;
            }
            else
            {
                recipt.ServiceId = service.Id;
                recipt.ReciptMasterId = Guid.Parse(extraParam);
                recipt.Quantity = service.Quantity;
                recipt.Description = service.Description;
                recipt.ServicePrice = service.Price;
                reciptMaster.TotalAmount += service.Price * service.Quantity;
                _dbContext.ReciptDetails.Add(recipt);
            }
            _dbContext.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(string key, string extraParam)
        {
            var reciptMaster = _dbContext.ReciptMasters.Find(Guid.Parse(extraParam));
            var reciptService = _dbContext.ReciptDetails
                                        .Where(x => x.ReciptMasterId == Guid.Parse(extraParam))
                                        .Where(x => x.ServiceId == Guid.Parse(key)).ToList().First();
            reciptMaster.TotalAmount -= reciptService.ServicePrice * reciptService.Quantity;
            _dbContext.Remove(reciptService);
            _dbContext.SaveChanges();
            return Ok();
        }

        public IActionResult GetPrice(Guid id)
        {
            var price = _dbContext.Services.FirstOrDefault(x => x.Id == id);
            return Ok(price.Price);
        }

        [HttpPost]
        public async Task<IActionResult> Conclude(Guid id)
        {
            try
            {
                var recipt = _dbContext.ReciptMasters.FirstOrDefault(x => x.Id == id);
                if (recipt != null)
                {
                    recipt.isInvoiced = true;
                    var supportTicket = _dbContext.SupportTickets.FirstOrDefault(x => x.Id == recipt.SupportTicketId);
                    supportTicket.ResolutionDate = DateTime.UtcNow;
                }
                _dbContext.SaveChanges();
                
                var user = _userManager.Users.FirstOrDefault(x => x.Id == recipt.UserId);
                var callbackUrl = Url.Action("Index", "Paymnet", new { id = recipt.Id}, protocol: Request.Scheme);

                var email = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Subject = "Your Invoice",
                    Body = $"You can pay your invoice by clicking <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click Here</a>",
                };
                await _emailSender.SendAsync(email);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Cancel(Guid id)
        {
            var reciptList = _dbContext.ReciptDetails.Where(x => x.ReciptMasterId == id).ToList();
            if (reciptList != null)
            {
                foreach (var item in reciptList)
                {
                    _dbContext.ReciptDetails.Remove(item);
                }
            }
            var reciptMaster = _dbContext.ReciptMasters.Find(id);
            _dbContext.ReciptMasters.Remove(reciptMaster);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
