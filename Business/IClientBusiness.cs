using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IClientBusiness
    {
        ClientDTO Create(ClientDTO client);
        ClientDTO FindById(Guid id);
        List<ClientDTO> FindAll();
        ClientDTO Update(ClientDTO client);
        void Delete(Guid id);
    }
}