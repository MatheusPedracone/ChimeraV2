using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.Repository.Users;

namespace Chimera_v2.Business.Implementations
{
    public class UserBusinessImplementation : IUserBusiness
    {
        private readonly IUserRepository _repository;

        public UserBusinessImplementation(IUserRepository repository)
        {
            _repository = repository;
        }
        public List<UserDTO> FindAll()
        {
            return _repository.GetAllUsers();
        }
        public UserDTO FindByUserName(string userName)
        {
             return _repository.GetUserByName(userName);
        }

        public UserDTO Login(UserDTO userDto)
        {
            return _repository.Login(userDto);
        }

        public UserDTO Singnup(UserDTO userDto)
        {
            return _repository.Signup(userDto);
        }
    }
}