namespace Chimera_v2.DTOs
{
    public class AdressDTO
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string County { get; set; }
        public string AdressNumber { get; set; }
        public string UF { get; set; }
        public ClientDTO Client { get; set; }
    }
}