using System.Threading.Tasks;
using Chimera_v2.DTOs;
using Chimera_v2.Repository;
using Chimera_v2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chimera_v2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDTO model)
        {
            var userLogin = await _userRepository.GetUserByUsername(model.Username);
            userLogin.Password = model.Password;
            var user = await _userRepository.ValidateUser(userLogin);

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
        public async Task<ActionResult<string>> Signup([FromBody] UserDTO model)
        {
            // Recupera o usuário
            var user = await _userRepository.CreateUser(model);

            // Gera o Token
            var token = _tokenService.GenerateToken(user);

            //retorna os dados
            return token;
        }
    }
}