using System.Threading.Tasks;
using Chimera_v2.Business;
using Chimera_v2.DTOs;
using Chimera_v2.Repository.Users;
using Chimera_v2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
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
        public IActionResult Login([FromBody] UserDTO userDto)
        {
            var userLogin = _userBusiness.FindByUserName(userDto.Username);
            userLogin.Password = userDto.Password;
            var user = _userBusiness.Login(userLogin);

            //verifica se o usuário existe
            if (user == default)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            //gera o token
            var token = _tokenService.GenerateToken(user);

            //retorna os dados
            return Ok(token);
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult Signup([FromBody] UserDTO userDto)
        {
            // Recupera o usuário
            var user = _userBusiness.Singnup(userDto);

            // Gera o Token
            var token = _tokenService.GenerateToken(user);

            //retorna os dados
            return Ok(token);
        }

        [HttpGet]
        [Route("FindAll")]
        public ActionResult FindAll()
        {
            return Ok(_userBusiness.FindAll());
        }

        [HttpGet]
        [Route("{userName}")]
        public ActionResult Get(string userName)
        {
            var user = _userBusiness.FindByUserName(userName);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}