using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.Models;

namespace Chimera_v2.Business
{
    public interface IUserBusiness
    {
        bool UserExists(string userName);
        List<UserDTO> GetAllUsers();
        UserLoginDto Login(UserLoginDto userLoginDto);
        UserLoginDto Register(UserLoginDto userLoginDto);
        User GetUserByUserName(string userName);
    }
}