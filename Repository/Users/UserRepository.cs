using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Chimera_v2.DTOs;
using Chimera_v2.Data;
using Chimera_v2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Chimera_v2.Repository.Users
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

        public async Task<UserDTO> ValidateUser(UserDTO userDto)
        {
            var validateUser = await GetUserByUsername(userDto.Username);
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

        public async Task<UserDTO> CreateUser(UserDTO userDto)
        {
            var userCreate = await GetUserByUsername(userDto.Username);
            if (userCreate != default)
            {
                throw new BadHttpRequestException("User already existis!");
            }

            await _context.Users.AddAsync(new User
            {
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password)
            });
            await _context.SaveChangesAsync();
            return new UserDTO
            {
                Username = userDto.Username,
                Password = userDto.Password
            };
        }

        public async Task<User> GetUserByUsernameTracking(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDto)
        {
            var user = await GetUserByUsername(userDto.Username);

            if (user == default)
            {
                return default;
            }
            user.Username = userDto.Username;
            user.Password = userDto.Password;
            await _context.SaveChangesAsync();
            return new UserDTO()
            {
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password)
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