using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.Models;

namespace Chimera_v2.Business
{
    public interface IUserBusiness
    {
        bool UserExists(string userName);
        List<UserDTO> GetAllUsers();
        UserDTO GetUserById(Guid id);
        User GetUserByUserName(string userName);
        UserLoginDto Login(UserLoginDto userLoginDto);
        UserLoginDto Register(UserLoginDto userLoginDto);
        UserLoginDto UpdateUser(UserLoginDto userLoginDto);
        void DeleteUser(Guid id);
    }
}