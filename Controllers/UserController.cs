using System;
using System.Linq;
using System.Threading.Tasks;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Chimera_v2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chimera_v2.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public UserController(AppDbContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserDTO userDto)
        {
            try
            {
                //vou buscar o usuario por username
                var user = _context
                .Users
                .AsNoTracking()
                .Select(u => new UserDTO
                {
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role
                })
                .Where(u => u.Username == userDto.Username)
                .FirstOrDefault();

                //verifico se user existe 
                if (user == null || BCrypt.Net.BCrypt.EnhancedVerify(userDto.Password, user.Password))
                {
                    //gera o token
                    var token = _tokenService.GenerateToken(user);
                    user.Password = "";
                    //Retorna os dados
                    return new
                    {
                        user = user,
                        token = token,
                        mesangem = "Autenticado com sucesso!"
                    };
                }
                else
                {
                    return NotFound(new { Erro = "Usuário ou senha inválidos!" });
                }
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Não foi possível realizar o login" });
            }
        }
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<dynamic>> Signup([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Erro = "Por favor verifique os dados digitados!" });
            }

            try
            {
                _context.Users.Add(new User
                {
                    Username = userDto.Username,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userDto.Password),
                    // Role = "Usuário"
                    Role = "Admin"
                });

                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
            return new
            {
                user = userDto,
                mensagem = "Usuário cadastrado com sucesso!"
            };
        }

         [HttpGet]
         [Route("getAll")]
         [Authorize(Roles = "Admin")]
         public ActionResult GetAll()
         {
                 var users = _context.Users.AsNoTracking().ToList();
                 if(users == null)
                     return NotFound(new { erro = "Não foi encontrado nenhum usuário!"});
                 return Ok(users);
         }

        // [HttpGet]
        // [Route("{id}")]
        // [Authorize(Roles = "Admin")]
        // public ActionResult Get(Guid Id)
        // {
        //     var user = _context.Users.AsNoTracking().Where(u => u.Id == Id).ToList();
        //     if (user == null) return NotFound();
        //     return Ok(user);
        // }
    }
}