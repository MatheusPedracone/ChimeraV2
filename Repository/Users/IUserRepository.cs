using System.Threading.Tasks;
using Chimera_v2.DTOs;

namespace Chimera_v2.Repository.Users
{
    public interface IUserRepository
    {
        // fazer depois GetUserByID 
        Task<UserDTO> GetUserByUsername(string userName);
        Task<UserDTO> ValidateUser(UserDTO userDto);
        Task<UserDTO> CreateUser(UserDTO userDto);
        Task<UserDTO> UpdateUser(UserDTO userDto);
        //fazer depois delete (guid id)
        Task DeleteUser(string userName);
    }
}