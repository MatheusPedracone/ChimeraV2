using System;
using System.ComponentModel.DataAnnotations;
using Chimera_v2.Models.Base;

namespace Chimera_v2.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}