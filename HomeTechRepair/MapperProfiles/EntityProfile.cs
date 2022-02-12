using AutoMapper;
using HomeTechRepair.Models.Entities;
using HomeTechRepair.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
