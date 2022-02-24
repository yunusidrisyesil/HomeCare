﻿using HomeTechRepair.Data;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;

namespace HomeTechRepair.Areas.Admin.Controllers.Apis
{
    [Route("api/[controller]/[action]")]
    public class ReportApiController : Controller
    {
        private readonly MyContext _dbContext;

        public ReportApiController(MyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult MonthlyIncome()
        {
            var monthlyList = _dbContext.ReciptMasters
                .Where(x => (x.Date >= DateTime.Now.AddYears(-1))).OrderBy(x => x.Date).ToList();
            var monthlyIncome = monthlyList.GroupBy(a => new { month = a.Date.Month })
                .Select(x => new ChartViewModel()
                {
                    y = x.Sum(x => x.TotalAmount).ToString(),
                    x = new CultureInfo("en-US").DateTimeFormat.GetMonthName(x.Key.month)
                }).ToList();
            return Ok(monthlyIncome);
        }
        public IActionResult LastMonthDaily()
        {
            var dailyList = _dbContext.ReciptMasters
                .Where(x => (x.Date >= DateTime.Now.AddMonths(-1))).OrderBy(x => x.Date).ToList();
                var dailyIncome = dailyList.GroupBy(a => new { day = a.Date.Day })
                .Select(x => new ChartViewModel()
                {
                    y = x.Sum(x => x.TotalAmount).ToString(),
                    x = x.Key.day.ToString()
                }).ToList();
            return Ok(dailyIncome);
        }
        public IActionResult NumbersInMonthly()
        {
            var monthlyList = _dbContext.ReciptMasters
            .Where(x => (x.Date >= DateTime.Now.AddYears(-1))).OrderBy(x => x.Date).ToList();
            var monthlyIncome = monthlyList.GroupBy(a => new { month = a.Date.Month })
                .Select(x => new ChartViewModel()
                {
                    y = x.Count().ToString(),
                    x = new CultureInfo("en-US").DateTimeFormat.GetMonthName(x.Key.month)
                }).ToList();
            return Ok(monthlyIncome);
        }
        public IActionResult NumbersInDaily()
        {
            var dailyList = _dbContext.ReciptMasters
    .Where(x => (x.Date >= DateTime.Now.AddMonths(-1))).OrderBy(x => x.Date).ToList();
            var dailyIncome = dailyList.GroupBy(a => new { day = a.Date.Day })
            .Select(x => new ChartViewModel()
            {
                y = x.Sum(x => x.TotalAmount).ToString(),
                x = x.Key.day.ToString()
            }).ToList();
            return Ok(dailyIncome);
        }
    }
}
