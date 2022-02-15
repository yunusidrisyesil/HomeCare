using AutoMapper;
using HomeTechRepair.Models.Payment;
using Iyzipay.Model;

namespace HomeTechRepair.MapperProfiles
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<InstallmentPriceModel, InstallmentPrice>().ReverseMap();
            CreateMap<InstallmentModel, InstallmentDetail>().ReverseMap();
            CreateMap<CardModel, PaymentCard>().ReverseMap();
            CreateMap<BasketModel, BasketItem>().ReverseMap();
            CreateMap<CustomerModel, Buyer>().ReverseMap();
            CreateMap<PaymentResponseModel, Payment>().ReverseMap();
            CreateMap<AddressModel, Address>().ReverseMap();
        }
    }
}
