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
        public async Task<ActionResult<dynamic>> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                //vou buscar o usuario por username
                var userContext = _context.Users
                .AsNoTracking()
                .Where(u => u.Username == userLoginDto.Username)
                .FirstOrDefault();

                //verifico se user existe 
                if (userContext == null || BCrypt.Net.BCrypt.EnhancedVerify(userLoginDto.Password, userContext.Password))
                {
                    //gera o token
                    var token = _tokenService.GenerateToken(userContext);
                    userContext.Password = "";
                    //Retorna os dados
                    return new
                    {
                        userContext = new UserDTO { Username = userContext.Username, Password = "", Role = userContext.Role },
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
        public async Task<ActionResult<dynamic>> Signup([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Erro = "Por favor verifique os dados digitados!" });
            }

            try
            {
                _context.Users.Add(new User
                {
                    Username = userLoginDto.Username,
                    Password = BCrypt.Net.BCrypt.EnhancedHashPassword(userLoginDto.Password),
                    Role = "Usuário"
                    // Role = "Admin"
                });

                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
            return new
            {
                user = userLoginDto,
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

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Get(Guid Id)
        {
            var user = _context.Users.AsNoTracking().Where(u => u.Id == Id).ToList();
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}