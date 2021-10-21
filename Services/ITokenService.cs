using Chimera_v2.DTOs;

namespace Chimera_v2.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO user);
    }
}