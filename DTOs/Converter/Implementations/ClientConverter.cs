using System.Collections.Generic;
using System.Linq;
using Chimera_v2.DTOs.Converter.Contract;
using Chimera_v2.Models;

namespace Chimera_v2.DTOs.Converter.Implementations
{
    public class ClientConverter : IParser<ClientDTO, Client>, IParser<Client, ClientDTO>
    {
        public Client Parse(ClientDTO origin)
        {
            if (origin == null) return null;
            return new Client
            {
                Name = origin.Name,
                CPF = origin.CPF,
                IE = origin.IE,
                ContributorType = origin.ContributorType,
                Email = origin.Email,
                Phone = origin.Phone,
                Adress = origin.AdressDTO,
            };
        }
        public ClientDTO Parse(Client origin)
        {
            if (origin == null) return null;
            return new ClientDTO
            {
                Name = origin.Name,
                CPF = origin.CPF,
                IE = origin.IE,
                ContributorType = origin.ContributorType,
                Email = origin.Email,
                Phone = origin.Phone
            };
        }
        public List<Client> Parse(List<ClientDTO> origin)
        {
            if(origin == null) return null;
            return origin.Select(item => Parse(item)).ToList(); 
        }
        public List<ClientDTO> Parse(List<Client> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}