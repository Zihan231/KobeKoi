using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.EF.Tables;

namespace KobeKoi.BLL
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Event, EventDTO>()
                .ForMember(d => d.VenueName, s => s.MapFrom(src => src.Venue.Name))
                .ForMember(d => d.VenueAddress, s => s.MapFrom(src => src.Venue.Address));
            CreateMap<EventDTO, Event>();
            CreateMap<CreateUserDTO, User>();
        }
    }
}