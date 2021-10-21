using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Chimera_v2.DTOs;
using Chimera_v2.Data;
using Chimera_v2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Chimera_v2.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetUserByUsername(string userName)
        {
            return await _context.Users.AsNoTracking().Select(x => new UserDTO
            {
                Username = x.Username,
                Password = x.Password,
            })
                .FirstOrDefaultAsync(x => x.Username == userName);
        }

        public async Task<UserDTO> ValidateUser(UserDTO user)
        {
            var validateUser = await GetUserByUsername(user.Username);
            if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, validateUser.Password))
            {
                return new UserDTO
                {
                    Username = validateUser.Username,
                    Password = validateUser.Password
                };
            }
            throw new AuthenticationException();
        }

        public async Task<UserDTO> CreateUser(UserDTO user)
        {
            var userCreate = await GetUserByUsername(user.Username);
            if (userCreate != default)
            {
                throw new BadHttpRequestException("User already existis!");
            }

            await _context.Users.AddAsync(new User
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password)
            });
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                Username = user.Username,
                Password = user.Password
            };
        }

        public async Task<User> GetUserByUsernameTracking(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var user1 = await GetUserByUsername(user.Username);

            if (user1 == default)
            {
                return default;
            }
            user1.Username = user.Username;
            user1.Password = user.Password;
            await _context.SaveChangesAsync();
            return new UserDTO()
            {
                Username = user.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password)
            };
        }

        public async Task DeleteUser(string userName)
        {
            var userEntity = await GetUserByUsernameTracking(userName);
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}