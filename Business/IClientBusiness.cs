using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IClientBusiness
    {
        ClientDTO Create(ClientDTO clientDto);
        ClientDTO FindById(Guid guid);
        List<ClientDTO> FindAll();
        ClientDTO Update(ClientDTO clientDto);
        void Delete(Guid guid);
    }
}