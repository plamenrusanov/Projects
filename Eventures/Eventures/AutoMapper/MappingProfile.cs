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
                .ForMember(dest => dest.When, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.RegularPrice, opt => opt.MapFrom(src => src.PricePerTicket))
                .ForMember(dest => dest.Available, opt => opt.MapFrom(src => src.TotalTickets))
                .ReverseMap();

            CreateMap<Event, EventViewModel>().ReverseMap();

            CreateMap<Event, EditEventViewModel>().ReverseMap();

            CreateMap<Ticket, MyEventViewModel>()
                .ForMember(dest => dest.AdultQuantity, opt => opt.MapFrom(src => src.Adult))
                .ForMember(dest => dest.ChildQuantity, opt => opt.MapFrom(src => src.Child))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Event.ImageUrl))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.Place, opt => opt.MapFrom(src => src.Event.Place))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Event.Start))
                .ReverseMap();
           
        }
    }
}
