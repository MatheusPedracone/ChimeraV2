using System;
using System.Collections.Generic;
using System.Linq;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Chimera_v2.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<UserDTO> GetAllUsers()
        {
            try
            {
                return _context.Users.Select(u => new UserDTO
                {
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar buscar usuários. Erro: {ex.Message}");
            }
        }
        public UserDTO GetUserById(Guid id)
        {
            try
            {
                return _context.Users
                .Where(u => u.Id.Equals(id))
                .Select(u => new UserDTO
                {
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                })
                .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar pegar Usuário por Identificação. Erro: {ex.Message}");
            }
        }
        public User GetUserByUserName(string userName)
        {
            try
            {
                return _context.Users.AsNoTracking().Select(u => new User
                {
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                }).FirstOrDefault(u => u.Username == userName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar pegar Usuário por Username. Erro: {ex.Message}");
            }
        }
        public UserLoginDto ValidateCredentials(UserLoginDto userLoginDto)
        {
            try
            {
                var userContext = GetUserByUserName(userLoginDto.Username);

                //verifico se user existe e se a senha é igual a do banco
                if (BC.Verify(userLoginDto.Password, userContext.Password))
                {
                    return new UserLoginDto
                    {
                        Username = userContext.Username,
                        Password = userContext.Password
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar fazer login. Erro: {ex.Message}");
            }
        }
        public UserLoginDto CreateUser(UserLoginDto userLoginDto)
        {
            try
            {
                _context.Users.Add(new User
                {
                    Username = userLoginDto.Username,
                    Password = BC.HashPassword(userLoginDto.Password),
                    Role = "Usuário"
                });
                _context.SaveChanges();
                return new UserLoginDto
                {
                    Username = userLoginDto.Username,
                    Password = ""
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar criar usuário. Erro: {ex.Message}");
            }
        }
        public UserLoginDto UpdateUser(UserLoginDto userLoginDto)
        {
            try
            {
                var user = GetUserByUserName(userLoginDto.Username);
                if (user == null) return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
            return null;
        }
        public void DeleteUser(Guid id)
        {
            var UserToDelete = _context.Users.Where(u => u.Id == id).First();

            _context.Remove(UserToDelete);
            _context.SaveChanges();
        }
        public bool UserExists(string userName)
        {
            try
            {
                return _context.Users.Any(u => u.Username == userName.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar se usuário existe. Erro: {ex.Message}");
            }
        }
    }
}