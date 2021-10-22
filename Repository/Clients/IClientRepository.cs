using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;


namespace Chimera_v2.Repository.Clients
{
    public interface IClientRepository
    {
        ClientDTO GetClient(Guid id);
        List<ClientDTO> GetAllClients();
    }
}