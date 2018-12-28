using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Eventures.Data.Models;
using Eventures.Models;

namespace Eventures.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEventViewModel, Event>().ReverseMap();
            CreateMap<Event, BuyTicketViewModel> ()
                .ForMember(dest => dest.Where, opt => opt.MapFrom(src => src.Place))
                .ForMember(dest => dest.When, src => src.MapFrom("Start"))
                .ForMember(dest => dest.RegularPrice, src => src.MapFrom("PricePerTicket"))
                .ForMember(dest => dest.Available, src => src.MapFrom("TotalTickets"))
                .ReverseMap();
            CreateMap<Event, EventViewModel>().ReverseMap();
            CreateMap<Event, EditEventViewModel>().ReverseMap();
           
        }
    }
}
