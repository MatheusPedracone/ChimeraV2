using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.DTOs.Converter.Implementations;
using Chimera_v2.Models;
using Chimera_v2.Repository.Generic;

namespace Chimera_v2.Business.Implementations
{
    public class ClientBusinessImplementations : IClientBusiness
    {
        private readonly IRepository<Client> _repository;

        private readonly ClientConverter _converter;

        public ClientBusinessImplementations(IRepository<Client> repository)
        {
            _repository = repository;
            _converter = new ClientConverter();
        }
        public ClientDTO Create(ClientDTO client)
        {
            var clientEntity = _converter.Parse(client);
            clientEntity = _repository.Create(clientEntity);
            return _converter.Parse(clientEntity);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }

        public List<ClientDTO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public ClientDTO FindById(Guid id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public ClientDTO Update(ClientDTO client)
        {
            var clientEntity = _converter.Parse(client);
            clientEntity = _repository.Update(clientEntity);
            return _converter.Parse(clientEntity);
        }
    }
}