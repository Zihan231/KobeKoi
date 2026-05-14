using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace KobeKoi.BLL.Service.Events
{
    public class EventService
    {
        private readonly EventRepo _eventRepo;
        private readonly Mapper _mapper;

        public EventService(EventRepo eventRepo)
        {
            _eventRepo = eventRepo;
            _mapper = MapperConfig.GetMapper();
        }

        public List<EventDTO> GetAllEvents()
        {
            var events = _eventRepo.GetAll().Include(e => e.Venue);

            var filteredEvents = events
        .Where(e => e.Status == 3
                 && e.VenuePaymentStatus == 1
                 && e.AvailableSeats > 0)
        .ToList();

        return _mapper.Map<List<EventDTO>>(filteredEvents);
        }
    }
}