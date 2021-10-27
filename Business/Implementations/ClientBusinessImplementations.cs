using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chimera_v2.DTOs;

using Chimera_v2.Repository.Clients;


namespace Chimera_v2.Business.Implementations
{
    public class ClientBusinessImplementations : IClientBusiness
    {
        private readonly IClientRepository _repository;

        public ClientBusinessImplementations(IClientRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ClientDTO>> FindAll()
        {
            return await _repository.GetAllClientsAsync();
        }
        public async Task<ClientDTO> FindById(Guid id)
        {
            return await _repository.GetClientAsync(id);
        }
        public async Task<ClientDTO> Create(ClientDTO clientDto)
        {
            return await _repository.CreateClient(clientDto);
        }
        public async Task Delete(Guid id)
        {
           await _repository.DeleteClient(id);
        }
        public async Task<ClientDTO> Update(Guid id)
        {
            return await _repository.UpdateClient(id);
        }
        public async Task<ClientDTO> Disable(Guid id)
        {
            return await _repository.Disable(id);
        }
    }
}