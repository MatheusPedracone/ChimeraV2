using System.Threading.Tasks;
using Chimera_v2.DTOs;

namespace Chimera_v2.Repository
{
    public interface IUserRepository
    {
        // fazer depois GetUserByID 
        Task<UserDTO> GetUserByUsername(string userName);
        Task<UserDTO> ValidateUser(UserDTO user);
        Task<UserDTO> CreateUser(UserDTO user);
        Task<UserDTO> UpdateUser(UserDTO user);
        //fazer depois delete (guid id)
        Task DeleteUser(string userName);
    }
}