using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.EF.Tables;

namespace KobeKoi.BLL
{
    public class MapperConfig
    {
        static MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Event, EventDTO>().ForMember(d=> d.VenueName , s => s.MapFrom(src => src.Venue.Name)).ForMember(d=> d.VenueAddress, s => s.MapFrom(src => src.Venue.Address));
            cfg.CreateMap<EventDTO, Event>();
        });

        public static Mapper GetMapper()
        {
            return new Mapper(config);
        }
    }
}