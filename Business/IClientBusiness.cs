using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IClientBusiness
    {
        List<ClientDTO> FindAll();
        ClientDTO FindById(Guid id);
        ClientDTO Create(ClientDTO clientDto);
        ClientDTO Update(ClientDTO clientDto);
        ClientDTO Disable(Guid id);
        void Delete(Guid id);
    }
}