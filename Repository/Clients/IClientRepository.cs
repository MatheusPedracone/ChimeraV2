using System;
using System.Collections.Generic;
using Chimera_v2.Models;

namespace Chimera_v2.Repository.Clients
{
    public interface IClientRepository
    {
        Client GetClient(Guid id);
        List<Client> GetAllClient();
    }
}