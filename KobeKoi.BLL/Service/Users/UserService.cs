using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.Service.Users
{
    public class UserService
    {
        EventRepo eventRepo;
        Mapper mapper;

        public UserService(EventRepo eventRepo, Mapper mapper)
        {
            this.eventRepo = eventRepo;
            this.mapper = mapper;
        }
        public List<EventDTO> GetAllEvents()
        {
            var events = eventRepo.GetAll();
            var eventDTOs = mapper.Map<List<EventDTO>>(events);
            return eventDTOs;
        }
    }
}
