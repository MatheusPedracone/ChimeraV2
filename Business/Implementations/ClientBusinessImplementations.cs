using System;
using System.Collections.Generic;
using Chimera_v2.DTOs;
using Chimera_v2.DTOs.Converter.Implementations;
using Chimera_v2.Repository.Clients;


namespace Chimera_v2.Business.Implementations
{
    public class ClientBusinessImplementations : IClientBusiness
    {
        private readonly IClientRepository _repository;
        private readonly ClientConverter _converter;

        public ClientBusinessImplementations(IClientRepository repository)
        {
           _repository = repository;
            _converter = new ClientConverter();
        }

        public ClientDTO Create(ClientDTO clientDto)
        {
            throw new NotImplementedException();
        }

        public ClientDTO FindById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public List<ClientDTO> FindAll()
        {
            throw new NotImplementedException();
        }

        public ClientDTO Update(ClientDTO clientDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}