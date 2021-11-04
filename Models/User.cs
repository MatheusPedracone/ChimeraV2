using Chimera_v2.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Chimera_v2.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}