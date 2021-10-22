
namespace Chimera_v2.DTOs
{
    public class ClientDTO
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string IE { get; set; }
        public string ContributorType { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AdressDTO Adress { get; set; }
    }
}