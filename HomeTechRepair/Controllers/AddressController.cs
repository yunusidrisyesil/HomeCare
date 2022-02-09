//ToDo stateId

using AutoMapper;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    [Route("Profile/[controller]/[action]")]
    public class AddressController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public AddressController(MyContext dbContext, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details()
        {

            var address = _dbContext.Addresses.FirstOrDefault(x => x.UserId == HttpContext.GetUserId());
            var model = _mapper.Map<AddressViewModel>(address);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(AddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var addres = _dbContext.Addresses.FirstOrDefault(x => x.UserId == HttpContext.GetUserId());
            if (addres is null)
            {
                try
                {
                    var newAddress = new Address()
                    {
                        UserId = HttpContext.GetUserId(),
                        BuildingNo = model.BuildingNo,
                        Description = model.Description,
                        DoorNo = model.DoorNo,
                        Line = model.Line,
                        StreetName = model.StreetName,
                        StateId = 1,
                    };

                    _dbContext.Addresses.Add(newAddress);
                    await _dbContext.SaveChangesAsync();


                    return View(model);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, "New Address cannot added. Please try again");
                    return View(model);
                }

            }
            try
            {
                var address = _dbContext.Addresses.FirstOrDefault(x => x.UserId == model.UserId);
                address.BuildingNo = model.BuildingNo;
                address.Description = model.Description;
                address.DoorNo = model.DoorNo;
                address.Line = model.Line;
                address.StreetName = model.StreetName;
                var result = await _dbContext.SaveChangesAsync();

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Address cannot changed. Please try again");
                return View(model);
            }

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var data = _dbContext.Addresses.FirstOrDefault(x => x.UserId == HttpContext.GetUserId());

                _dbContext.Addresses.Remove(data);
                var result = await _dbContext.SaveChangesAsync();
                return RedirectToAction("Details", "Address");

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Address cannot deleted. Please try again");
                return RedirectToAction("Details", "Address");
            }

        }
    }
}
