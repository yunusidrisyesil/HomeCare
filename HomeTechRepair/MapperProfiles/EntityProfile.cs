using AutoMapper;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;

namespace HomeTechRepair.MapperProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<AddressViewModel, Address>().ReverseMap();
            CreateMap<UserTicketViewModel, SupportTicket>().ReverseMap();
        }
    }
}
