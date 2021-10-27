using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;


namespace Chimera_v2.Repository.Clients
{
    public interface IClientRepository
    {
       Task<ClientDTO> GetClientAsync(Guid id);
       Task<List<ClientDTO>> GetAllClientsAsync();
       Task<ClientDTO> CreateClient(ClientDTO clientDto);
       Task<ClientDTO> UpdateClient(Guid id);
       Task<ClientDTO> Disable(Guid id);
       Task DeleteClient(Guid id);
    }
}