using AutoMapper;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.Models.Payment;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTechRepair.Models.Services.Payment
{
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public IyzicoPaymentService(IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
        }

        public string PaymentChannel { get; private set; }
        public string PaymentGroup { get; private set; }
        public PaymentCard PaymentCard { get; private set; }

        private string GenerateConversationId()
        {
            Random rnd = new Random();
            var Id = rnd.Next(1000000, 9999999).ToString();
            return Id;
        }
        private CreatePaymentRequest InitialPaymentRequest(PaymentModel model)
        {
            var paymentrequest = new CreatePaymentRequest
            {
                Installment = model.Installment,
                Locale = Locale.TR.ToString(),
                ConversationId = GenerateConversationId(),
                Price = model.Price.ToString(new CultureInfo("en-US")),
                PaidPrice = model.PaidPrice.ToString(new CultureInfo("en-US")),
                Currency = Currency.TRY.ToString(),
                BasketId = GenerateConversationId(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.SUBSCRIPTION.ToString(),
                PaymentCard = _mapper.Map<PaymentCard>(model.CardModel)
            };
            var user = _userManager.FindByIdAsync(model.UserId).Result;
            var buyer = new Buyer
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                GsmNumber = user.PhoneNumber,
                Email = user.Email,
                IdentityNumber = "11111111110",
                LastLoginDate = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                RegistrationDate = $"{user.CreatedDate:yyyy-MM-dd HH:mm:ss}",
                RegistrationAddress = "Cihannuma Mah. Barbaros Bulvarı No:9 Beşiktaş",
                Ip = model.Ip,
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34752"
            };
            paymentrequest.Buyer = buyer;

            var billingAddress = new Address
            {
                ContactName = $"{user.Name} {user.Surname}",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Cihannuma Mah. Barbaros Bulvarı No:9 Beşiktaş",
                ZipCode = "34752"
            };
            paymentrequest.BillingAddress = billingAddress;

            var basketItems = new List<BasketItem>();
            var firstBasketItem = new BasketItem
            {
                Id = "BI101",
                Name = "Binocular",
                Category1 = "Collectibles",
                Category2 = "Accessories",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = model.Price.ToString(new CultureInfo("en-US"))
            };
            basketItems.Add(firstBasketItem);
            paymentrequest.BasketItems = basketItems;
            return paymentrequest;
        }
        public InstallmentModel CheckInstalment(string binNumber, decimal Price)
        {
            throw new NotImplementedException();
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
