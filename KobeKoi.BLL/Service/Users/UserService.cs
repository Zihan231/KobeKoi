using AutoMapper;
using KobeKoi.BLL.DTO;
using KobeKoi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.Service.Users
{
    public class UserService
    {
        private readonly UserRepo _userRepo;
        private readonly EventRepo _eventRepo;
        private readonly IMapper _mapper;


        public UserService(UserRepo userRepo, EventRepo eventRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _eventRepo = eventRepo;
            _mapper = mapper;
        }

        
    }
}
