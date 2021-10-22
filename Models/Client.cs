using Chimera_v2.DTOs;
using Chimera_v2.Models.Base;

namespace Chimera_v2.Models
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string IE { get; set; }
        public string ContributorType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Adress Adress { get; set; }
    }
}