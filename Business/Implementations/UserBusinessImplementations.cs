using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Chimera_v2.Repository.Users;

namespace Chimera_v2.Business.Implementations
{
    public class UserBusinessImplementations : IUserBusiness
    {
        private readonly IUserRepository _repository;

        public UserBusinessImplementations(IUserRepository repository)
        {
            _repository = repository;
        }
        public List<UserDTO> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }
        public User GetUserByUserName(string userName)
        {
            return _repository.GetUserByUserName(userName);
        }
        public UserLoginDto Login(UserLoginDto userLoginDto)
        {
            return _repository.ValidateCredentials(userLoginDto);
        }

        public UserLoginDto Register(UserLoginDto userLoginDto)
        {
            return _repository.CreateUser(userLoginDto);
        }

        public bool UserExists(string userName)
        {
            return  _repository.UserExists(userName);
        }
    }
}