using System.ComponentModel.DataAnnotations;

namespace Chimera_v2.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}