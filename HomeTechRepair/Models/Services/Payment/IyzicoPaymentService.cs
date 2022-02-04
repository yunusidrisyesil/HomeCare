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
        //todo adres vb bilgiler düzenlenebilir
        private readonly IConfiguration _configuration;
        private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public IyzicoPaymentService(IConfiguration configuration, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;

            var section = _configuration.GetSection(IyzicoPaymentOptions.Key);
            _options = new IyzicoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey = section["SecretKey"],
                BaseUrl = section["BaseUrl"],
                ThreedsCallbackUrl = section["ThreedsCallbackUrl "]
            };
        }
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
            if (binNumber.Length > 6)
                binNumber = binNumber.Substring(0, 6);
            var conversationId = GenerateConversationId();
            var request = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = conversationId,
                BinNumber = binNumber,
                Price = Price.ToString(new CultureInfo("en-US")),
            };
            var result = InstallmentInfo.Retrieve(request, _options);
            if (result.Status == "Failure")
            {
                throw new Exception(result.ErrorMessage);
            }
            if (result.ConversationId != conversationId)
            {
                throw new Exception("Hatalı istek oluşturuldu.");
            }
            var resultModel = _mapper.Map<InstallmentModel>(result.InstallmentDetails[0]);

            return resultModel;
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            var request = InitialPaymentRequest(model);
            var payment = Payment.Create(request, _options);
            return _mapper.Map<PaymentResponseModel>(payment);
        }
    }
}
