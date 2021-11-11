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
using Chimera_v2.Business;
using Microsoft.AspNetCore.Http;

namespace Chimera_v2.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserBusiness _userBusiness;

        public UserController(ITokenService tokenService, IUserBusiness userBusiness)
        {
            _tokenService = tokenService;
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                var userContext = _userBusiness.GetUserByUserName(userLoginDto.Username);
                if (userContext == null)
                    return Unauthorized(new { Erro = "Usuário ou senha inválidos!" });

                var userToLogin = _userBusiness.Login(userLoginDto);
                var token = _tokenService.GenerateToken(userContext);
                userContext.Password = "";
                return Ok(new
                {
                    userContext = new UserDTO { Username = userContext.Username, Password = "", Role = userContext.Role },
                    token = token,
                    mesangem = "Autenticado com sucesso!"
                });
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Não foi possível realizar o login" });
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<dynamic>> Register([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                _userBusiness.Register(userLoginDto);
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Erro ao tentar Registrar Usuário!" });
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
            try
            {
                var users = _userBusiness.GetAllUsers();
                if (users == null)
                    return NotFound(new { erro = "Não foi encontrado nenhum usuário!" });
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest(new { Erro = "Não foi possível buscar usuário" });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Get(Guid Id)
        {
            try
            {
                var user = _userBusiness.GetUserById(Id);
                if (user == null)
                    return NotFound(new { erro = "Não foi encontrado nenhum usuário!" });
                return Ok(user);
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Não foi possível buscar usuário" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid Id)
        {
            try
            {
                _userBusiness.DeleteUser(Id);
                return NoContent();
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Não foi possível deletar usuário" });
            }
        }
    }
}