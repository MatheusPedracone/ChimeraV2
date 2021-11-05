using System.Threading.Tasks;
using Chimera_v2.Business;
using Chimera_v2.DTOs;
using Chimera_v2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ITokenService _tokenService;
        public UserController(IUserBusiness userBusiness, ITokenService tokenService)
        {
            _userBusiness = userBusiness;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserDTO userDto)
        {
            try
            {
                var userLogin = _userBusiness.FindByUserName(userDto.Username);
                if (userLogin == null)
                {
                    return NotFound(new { Erro = "Usuário não encontrado!" });
                }
                else
                {
                    if (BCrypt.Net.BCrypt.EnhancedVerify(userDto.Password, userLogin.Password))
                    {
                        var user = _userBusiness.Login(userLogin);
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
                        return NotFound(new { Erro = "Senha inválida!" });
                    }
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
                userDto.Role = "Usuário";
                _userBusiness.Singnup(userDto);
            }
            catch (System.Exception)
            {
                return BadRequest(new { Erro = "Não foi possível se conectar com o banco de dados!" });
            }
            return new
            {
                mensagem = "Usuário cadastrado com sucesso!"
            };
        }

        [HttpGet]
        [Route("FindAll")]
        [Authorize]
        public ActionResult FindAll()
        {
            return Ok(_userBusiness.FindAll());
        }

        [HttpGet]
        [Route("{userName}")]
        [Authorize]
        public ActionResult Get(string userName)
        {
            var user = _userBusiness.FindByUserName(userName);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}