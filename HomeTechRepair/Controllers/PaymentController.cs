using HomeTechRepair.Models.Payment;
using HomeTechRepair.Models.Services.Payment;
using HomeTechRepair.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeTechRepair.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public IActionResult Index()
        {
            return View();
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
                Price = 1000,
                UserId = "1",
                Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString()
            };
            //var installmentInfo = _paymentService.CheckInstalment(paymentModel.CardModel.CardNumber, paymentModel.Price);
            //var installmentNumber = installmentInfo.InstallmentPrices.FirstOrDefault(x => x.InstallmentNumber == model.Installment);
            //paymentModel.PaidPrice = decimal.Parse(installmentNumber != null ? installmentNumber.TotalPrice.Replace('.', ',') : installmentInfo.InstallmentPrices[0].TotalPrice.Replace('.', ','));


            var result = _paymentService.Pay(paymentModel);
            return View();
        }
    }
}
