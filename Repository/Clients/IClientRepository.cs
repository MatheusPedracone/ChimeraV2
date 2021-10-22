using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;


namespace Chimera_v2.Repository.Clients
{
    public interface IClientRepository
    {
       Task<ClientDTO> GetClientAsync(Guid id);
       Task<List<ClientDTO>> GetAllClients();
       Task<ClientDTO> CreateClient(ClientDTO clientDto);
       Task<ClientDTO> UpdateClient(ClientDTO clientDto);
       Task DeleteClient(string name);

    }
}