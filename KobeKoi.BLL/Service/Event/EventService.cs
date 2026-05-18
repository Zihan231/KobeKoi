using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.EF.Tables;
using KobeKoi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            var filteredEvents = events.Where(e => e.Status == 3 && e.VenuePaymentStatus == 1 && e.AvailableSeats > 0)
        .ToList();

            return _mapper.Map<List<EventDTO>>(filteredEvents);
        }

        //Search events by title
        public List<EventDTO> SearchAndFilterEvents(string searchTerm, string filterOption)
        {
            var query = _eventRepo.GetAll().Include(e => e.Venue).AsQueryable();

            query = query.Where(e => e.Status == 3 && e.VenuePaymentStatus == 1 && e.AvailableSeats > 0);

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

        //Create events
        public bool CreateEvent(CreateEventDTO dto, int organizerId)
        {
            if (dto == null)
            {
                return false;
            }

            var ev = new Event
            {
                Title = dto.Title,
                EventDate = dto.EventDate,
                MaxCapacity = dto.MaxCapacity,
                AvailableSeats = dto.MaxCapacity,
                VenueId = dto.VenueId,
                OrganizerId = organizerId,
                Status = 1,
                VenuePaymentStatus = 0,
                TicketPrice = dto.TicketPrice
            };

            try
            {
                _eventRepo.Add(ev);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Events By organizere ID
        public List<EventDTO> GetEventsByOrganizerId(int organizerId)
        {
            var events = _eventRepo.GetAll().Where(e => e.OrganizerId == organizerId).Include(e => e.Venue).ToList();
            return _mapper.Map<List<EventDTO>>(events);
        }

       
        public bool PayVenue(int eventId)
        {
            var ev = _eventRepo.GetAll().FirstOrDefault(e => e.Id == eventId);

            if (ev == null)
                return false;

            ev.VenuePaymentStatus = 1;

            _eventRepo.Update(ev);

            return true;
        }
    }
}