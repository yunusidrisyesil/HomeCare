using HomeTechRepair.Data;
using HomeTechRepair.Extensions;
using HomeTechRepair.Models.Payment;
using HomeTechRepair.Services;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeTechRepair.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly MyContext _dbContext;
        public PaymentController(IPaymentService paymentService, MyContext dbContext)
        {
            _paymentService = paymentService;
            _dbContext = dbContext;
        }
        public IActionResult Index(Guid id)
        {
            var data = _dbContext.ReciptMasters.FirstOrDefault(x => x.Id == id);
            var model = new PaymentViewModel();
            model.PaidAmount = (decimal)data.TotalAmount;
            ViewBag.PaidAmount = model.PaidAmount;
            return View(model);
        }
        [HttpPost]
        public IActionResult CheckInstallment(string binNumber, decimal price)
        {
            var result = _paymentService.CheckInstalment(binNumber, price);
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Index(PaymentViewModel model)
        {
            var paymentModel = new PaymentModel()
            {
                Installment = model.Installment,
                Address = new AddressModel(),
                BasketList = new List<BasketModel>(),
                Customer = new CustomerModel(),
                CardModel = model.CardModel,
                Price = model.PaidAmount,
                UserId = HttpContext.GetUserId(),
                Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString()
            };
            var installmentInfo = _paymentService.CheckInstalment(paymentModel.CardModel.CardNumber, paymentModel.Price);

            var installmentNumber =
                installmentInfo.InstallmentPrices.FirstOrDefault(x => x.InstallmentNumber == model.Installment);

            paymentModel.PaidPrice = decimal.Parse(installmentNumber != null ? installmentNumber.TotalPrice.Replace('.', ',') : installmentInfo.InstallmentPrices[0].TotalPrice.Replace('.', ','));
            var result = _paymentService.Pay(paymentModel);
            return View();
        }
    }
}
