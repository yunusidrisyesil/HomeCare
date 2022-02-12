﻿using AutoMapper;
using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;
        public TicketController(UserManager<ApplicationUser> userManager, MyContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(HttpContext.GetUserId());
            if (user != null)
            {
                try
                {
                    _dbContext.SupportTickets.Add(new SupportTicket
                    {
                        Description = model.Description,
                        UserId = user.Id,
                    });
                    _dbContext.SaveChanges();
                    ViewBag.Message = "Support ticket created succesfully";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(model);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> GetTicketsAsync()
        {
            var ticketList = _dbContext.SupportTickets.Where(x => x.UserId == HttpContext.GetUserId());
            List<UserTicketViewModel> model = new List<UserTicketViewModel>();
            foreach (var item in ticketList)
            {
                var data = _mapper.Map<UserTicketViewModel>(item);
                if (item.DoctorId != null)
                {
                    var doctor = await _userManager.FindByIdAsync(item.DoctorId);
                    data.DoctorName = doctor.Name;
                }
                model.Add(data);
            }
            ViewBag.DataSource = model;
            return View();
        }
    }
}