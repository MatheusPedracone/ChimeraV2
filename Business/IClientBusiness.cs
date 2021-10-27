using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IClientBusiness
    {
        Task<List<ClientDTO>> FindAll();
        Task<ClientDTO> FindById(Guid id);
        Task<ClientDTO> Create(ClientDTO clientDto);
        Task<ClientDTO> Update(Guid id);
        Task<ClientDTO> Disable(Guid id);
        Task Delete(Guid id);
    }
}