using KobeKoi.BLL.DTO;
using KobeKoi.DAL.EF.Tables;
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


        //Login service
        public string AuthenticateUser(LoginDTO loginDto)
        {
            if (loginDto == null ||
                string.IsNullOrWhiteSpace(loginDto.Email) ||
                string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return "Invalid login parameters.";
            }

            var user = _userRepo.GetAll().FirstOrDefault(u => u.Email.ToLower() == loginDto.Email.ToLower());

            if (user == null)
            {
                return "Invalid email or password.";
            }

            if (user.Status == "Locked")
            {
                return "Your account is locked. Contact admin.";
            }

            if (user.Password == loginDto.Password)
            {
                user.TotalAttempts = 0;
                _userRepo.Update(user);

                return "Success";
            }

            user.TotalAttempts = (user.TotalAttempts ?? 0) + 1;

            if (user.TotalAttempts >= 5)
            {
                user.Status = "Locked";
            }

            _userRepo.Update(user);

            return $"Invalid password. Attempt {user.TotalAttempts}/5";
        }

        //signUp service
        public bool CreateUser(CreateUserDTO dto)
        {
            if (dto == null)
            {
                return false;
            }
                
            var existingUser = _userRepo.GetAll().FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower());

            if (existingUser != null)
            {
                return false;
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                CreatedAt = DateTime.Now
            };

            _userRepo.Add(user); 

            return true;
        }
    }
}
