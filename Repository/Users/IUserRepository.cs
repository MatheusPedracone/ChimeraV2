using System;
using System.Threading.Tasks;
using Chimera_v2.DTOs;

namespace Chimera_v2.Repository.Users
{
    public interface IUserRepository
    {
        // depois fazer GetUserById(Guid guid)
        Task<UserDTO> GetUserByUsername(string userName);
        Task<UserDTO> ValidateUser(UserDTO userDto);
        Task<UserDTO> CreateUser(UserDTO userDto);
        Task<UserDTO> UpdateUser(UserDTO userDto);
        //depois fazer delete(Guid guid)
        Task DeleteUser(Guid guid);
    }
}