using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.Service.Admin
{
    public class AdminService
    {
        private readonly UserRepo _userRepo;
        private readonly EventRepo _eventRepo;
        private readonly IMapper _mapper;


        public AdminService(UserRepo userRepo, EventRepo eventRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _eventRepo = eventRepo;
            _mapper = mapper;
        }
        //Show Admin Stats
        public AdminStatsDTO GetAdminStats()
        {
            var users = _userRepo.GetAll();
            var events = _eventRepo.GetAll();

            var stats = new AdminStatsDTO
            {
                TotalUsers = users.Count(),
                TotalEvents = events.Count(),

                PendingEvents = events.Count(e => e.Status == 1),
                LiveEvents = events.Count(e => e.Status == 3),
                RejectedEvents = events.Count(e => e.Status == 2),

                LockedUsers = users.Count(u => u.Status == "Locked"),

                RecentEvents = events
                    .Where(e => e.Status == 3)
                    .OrderByDescending(e => e.Id)
                    .Take(3)
                    .Select(e => new EventDTO
                    {
                        Id = e.Id,
                        Title = e.Title,
                        EventDate = e.EventDate,
                        TicketPrice = e.TicketPrice,
                        AvailableSeats = e.AvailableSeats,
                        MaxCapacity = e.MaxCapacity,
                        VenueName = e.Venue.Name,
                        VenueAddress = e.Venue.Address
                    })
                    .ToList(),

                RecentUsers = users
                    .OrderByDescending(u => u.Id)
                    .Take(3)
                    .Select(u => new UserDTO
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email
                    })
                    .ToList()
            };

            return stats;
        }
        //See all Events
        public List<EventDTO> GetAllEvents()
        {

            var events = _eventRepo.GetAll().Include(e => e.Venue);

           

            var filteredEvents = _eventRepo.GetAll()
                .Include(e => e.Venue)
                .Include(e => e.Organizer)
                .OrderByDescending(e => e.Id)
                .ToList();

            return _mapper.Map<List<EventDTO>>(filteredEvents);
        }

        //Update Status of Events
        public void UpdateEventStatus(int id, int status)
        {
            var ev = _eventRepo.GetAll().FirstOrDefault(e => e.Id == id);

            if (ev == null) return;

            ev.Status = status;

            _eventRepo.Update(ev);
        }

        //Delete events
        public void DeleteEvent(int id)
        {
            var ev = _eventRepo.GetAll().FirstOrDefault(e => e.Id == id);

            if (ev == null) return;

            _eventRepo.Delete(ev);
        }
    }
}
