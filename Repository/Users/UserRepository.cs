using System.Linq;
using System.Security.Authentication;
using Chimera_v2.DTOs;
using Chimera_v2.Data;
using Chimera_v2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Chimera_v2.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<UserDTO> GetAllUsers()
        {
            return _context.Users.Select(c => new UserDTO
            {
                Username = c.Username,
                Password = c.Password
            })
            .ToList();
        }
        public UserDTO GetUserByName(string Username)
        {
            return _context.Users.AsNoTracking().Select(x => new UserDTO
            {
                Username = x.Username,
                Password = x.Password
            })
                .FirstOrDefault(x => x.Username == Username);
        }
        public UserDTO Login(UserDTO userDto)
        {
            var validateUser = _context.Users.FirstOrDefault(u => u.Username == userDto.Username);

            if (BCrypt.Net.BCrypt.EnhancedVerify(userDto.Password, validateUser.Password))
            {
                return new UserDTO
                {
                    Username = validateUser.Username,
                    Password = validateUser.Password
                };
            }
            throw new AuthenticationException();
        }
        public UserDTO Signup(UserDTO userDto)
        {
            var userCreate = _context.Users.FirstOrDefault(u => u.Username == userDto.Username);
            if (userCreate != default)
            {
                throw new BadHttpRequestException("User already existis!");
            }

            _context.Users.Add(new User
            {
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password)
            });
            _context.SaveChanges();
            return new UserDTO
            {
                Username = userDto.Username,
                Password = userDto.Password
            };
        }
    }
}