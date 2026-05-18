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
        private readonly IMapper _mapper;

        public EventService(EventRepo eventRepo, IMapper mapper)
        {
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        //See all events
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

        //Search events by title
        public List<EventDTO> SearchAndFilterEvents(string searchTerm, string filterOption)
        {
            var query = _eventRepo.GetAll().Include(e => e.Venue).AsQueryable();

            query = query.Where(e => e.Status == 3
                                  && e.VenuePaymentStatus == 1
                                  && e.AvailableSeats > 0);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTermLower = searchTerm.ToLower();
                query = query.Where(e => e.Title.ToLower().Contains(searchTermLower));
            }

            if (!string.IsNullOrWhiteSpace(filterOption))
            {
                if (filterOption == "Highest Price")
                {
                    query = query.OrderByDescending(e => e.TicketPrice);
                }
                else if (filterOption == "Lowest Price")
                {
                    query = query.OrderBy(e => e.TicketPrice);
                }
                else if (filterOption == "Newest")
                {
                    query = query.OrderByDescending(e => e.Id);
                }
                else if (filterOption == "Today")
                {
                    query = query.Where(e => e.EventDate.Date == DateTime.Today);
                }
            }
            else
            {
                query = query.OrderBy(e => e.EventDate);
            }

            var finalResults = query.ToList();

            return _mapper.Map<List<EventDTO>>(finalResults);
        }

        //
    }
}