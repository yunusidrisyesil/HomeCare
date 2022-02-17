using AutoMapper;
using HomeTechRepair.Areas.Admin.ViewModels;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.Models.Identiy;
using HomeTechRepair.ViewModels;

namespace HomeTechRepair.MapperProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<AddressViewModel, Address>().ReverseMap();
            CreateMap<UserTicketViewModel, SupportTicket>().ReverseMap();
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
        }
    }
}
