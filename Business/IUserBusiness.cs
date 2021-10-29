using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IUserBusiness
    {
        List<UserDTO> FindAll();
        UserDTO FindByUserName(string userName);
        UserDTO Login(UserDTO userDto);
        UserDTO Singnup(UserDTO userDto);
    }
}