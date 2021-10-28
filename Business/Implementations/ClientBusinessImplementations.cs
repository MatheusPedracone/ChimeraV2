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
        public List<ClientDTO> FindAll()
        {
            return  _repository.GetAllClients();
        }

        public ClientDTO FindById(Guid id)
        {
            return  _repository.GetClient(id);
        }

        public ClientDTO Create(ClientDTO clientDto)
        {
            return  _repository.CreateClient(clientDto);
        }

        public ClientDTO Update(ClientDTO clientDto)
        {
            return  _repository.UpdateClient(clientDto);
        }

       public ClientDTO Disable(Guid id)
        {
            return  _repository.Disable(id);
        }

        public void Delete(Guid id)
        {
            _repository.DeleteClient(id);
        }
    }
}