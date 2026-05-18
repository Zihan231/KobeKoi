using KobeKoi.BLL.DTO;
using KobeKoi.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KobeKoi.BLL.Service.Auth
{
    public class AuthService
    {
        private readonly UserRepo _userRepo;

        public AuthService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public string AuthenticateUser(LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return "Invalid login parameters.";
            }

            var user = _userRepo.GetAll().FirstOrDefault(u => u.Email.ToLower() == loginDto.Email.ToLower());

            if (user != null && user.Password == loginDto.Password)
            {
                return "Success";
            }

            return "Invalid email or password.";
        }
    }
}
