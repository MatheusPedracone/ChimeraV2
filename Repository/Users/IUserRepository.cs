using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;

namespace Chimera_v2.Repository.Users
{
    public interface IUserRepository
    {
        UserDTO GetUserByName(string userName);
        List<UserDTO> GetAllUsers();
        UserDTO Login(UserDTO userDto);
        UserDTO Signup(UserDTO userDto);
    }
}