using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;


namespace Chimera_v2.Repository.Clients
{
    public interface IClientRepository
    {
       ClientDTO GetClient(Guid id);
       List<ClientDTO> GetAllClients();
       ClientDTO CreateClient(ClientDTO clientDto);
        ClientDTO UpdateClient(ClientDTO clientDto);
       ClientDTO Disable(Guid id);
       void DeleteClient(Guid id);
    }
}