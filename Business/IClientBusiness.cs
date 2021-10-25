using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;

namespace Chimera_v2.Business
{
    public interface IClientBusiness
    {
        Task<ClientDTO> Create(ClientDTO clientDto);
        Task<ClientDTO> FindById(Guid id);
        Task<List<ClientDTO>> FindAll();
        Task<ClientDTO> Update(ClientDTO clientDto);
        Task Delete(string name);
    }
}