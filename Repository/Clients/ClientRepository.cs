using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Chimera_v2.Repository.Clients
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        public Client GetByNameTracking(string name)
        {
            return _context.Clients
                .FirstOrDefault(c => c.Name == name);
        }
        public Client GetByIdTracking(Guid id)
        {
            return _context.Clients
                .FirstOrDefault(c => c.Id == id);
        }
        public ClientDTO GetClient(Guid id)
        {
            var client = _context.Clients
               .Include(c => c.Adress)
               .Where(c => c.Id.Equals(id))
               .Select(c => new ClientDTO
               {
                   Name = c.Name,
                   CPF = c.CPF,
                   IE = c.IE,
                   ContributorType = c.ContributorType,
                   Email = c.Email,
                   Phone = c.Phone,
                   Enabled = c.Enabled,
                   Adress = new AdressDTO
                   {
                       ZipCode = c.Adress.ZipCode,
                       Street = c.Adress.Street,
                       District = c.Adress.District,
                       County = c.Adress.County,
                       AdressNumber = c.Adress.AdressNumber,
                       UF = c.Adress.UF
                   }
               })
               .FirstOrDefault();

            return client ?? null;
        }
        public List<ClientDTO> GetAllClients()
        {
            return _context.Clients
              .Include(c => c.Adress)
              .Select(c => new ClientDTO
              {
                  Name = c.Name,
                  CPF = c.CPF,
                  IE = c.IE,
                  ContributorType = c.ContributorType,
                  Email = c.Email,
                  Phone = c.Phone,
                  Enabled = c.Enabled,
                  Adress = new AdressDTO
                  {
                      ZipCode = c.Adress.ZipCode,
                      Street = c.Adress.Street,
                      District = c.Adress.District,
                      County = c.Adress.County,
                      AdressNumber = c.Adress.AdressNumber,
                      UF = c.Adress.UF
                  }
              })
              .ToList();
        }
        public ClientDTO CreateClient(ClientDTO clientDto)
        {
            var client = GetByNameTracking(clientDto.Name);

            if (client != default)
            {
                throw new BadHttpRequestException("Client already existis!");
            }
            _context.Clients.Add(new Client
            {
                Name = clientDto.Name,
                CPF = clientDto.CPF,
                IE = clientDto.IE,
                ContributorType = clientDto.ContributorType,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Enabled = clientDto.Enabled,
                Adress = new Adress
                {
                    ZipCode = clientDto.Adress.ZipCode,
                    Street = clientDto.Adress.Street,
                    District = clientDto.Adress.District,
                    County = clientDto.Adress.County,
                    AdressNumber = clientDto.Adress.AdressNumber,
                    UF = clientDto.Adress.UF
                }
            });
            _context.SaveChanges();

            return new ClientDTO
            {
                Name = client.Name,
                CPF = client.CPF,
                IE = client.IE,
                ContributorType = client.ContributorType,
                Email = client.Email,
                Phone = client.Phone,
                Enabled = client.Enabled,
                Adress = new AdressDTO
                {
                    ZipCode = client.Adress.ZipCode,
                    Street = client.Adress.Street,
                    District = client.Adress.District,
                    County = client.Adress.County,
                    AdressNumber = client.Adress.AdressNumber,
                    UF = client.Adress.UF
                }
            };
        }
        public ClientDTO UpdateClient(ClientDTO clientDto)
        {
            // aqui eu declarei o Client do banco, e dentro dele eu busco por Id e incluo o Adress
            var clientOrigin = _context.Clients
            .Where(c => c.Id == clientDto.Guid)
            .Include(c => c.Adress)
            .SingleOrDefault();

            // se o client que eu busquei por Id for dirente de null, eu vou fazer o update 
            if (clientOrigin != null)
            {
                // realizo o update
                _context.Entry(clientOrigin).CurrentValues.SetValues(clientDto);


                // qui eu declarei o Adress do banco, e dentro dele eu busco por guid e incluo o client
                var adressOrigin = _context.Adresses
                    .Where(c => c.Id == clientDto.Adress.Guid)
                    .Include(c => c.Client)
                    .SingleOrDefault();

                //se o guid for diferente de null, eu vou fazer o update
                if (clientDto.Adress.Guid != null)
                {
                    // Update adress
                    _context.Entry(adressOrigin).CurrentValues.SetValues(clientDto.Adress);
                }
                else
                {   // add adress
                    var newAdress = new AdressDTO
                    {
                        ZipCode = adressOrigin.ZipCode,
                        Street = adressOrigin.Street,
                        District = adressOrigin.District,
                        County = adressOrigin.County,
                        AdressNumber = adressOrigin.AdressNumber,
                        UF = adressOrigin.UF
                    };
                    _context.Adresses.Add(adressOrigin);
                }
            }
            _context.SaveChanges();
         
         return new ClientDTO
            {
                Name = clientDto.Name,
                CPF = clientDto.CPF,
                IE = clientDto.IE,
                ContributorType = clientDto.ContributorType,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
                Enabled = clientDto.Enabled,
                Adress = new AdressDTO
                {
                    ZipCode = clientDto.Adress.ZipCode,
                    Street = clientDto.Adress.Street,
                    District = clientDto.Adress.District,
                    County = clientDto.Adress.County,
                    AdressNumber = clientDto.Adress.AdressNumber,
                    UF = clientDto.Adress.UF
                }
            };
            
        }

        public ClientDTO Disable(Guid id)
        {
            if (!_context.Clients.Any(c => c.Id.Equals(id))) return null;
            var clientStatus = _context.Clients.SingleOrDefault(c => c.Id.Equals(id));

            if (clientStatus != null)
            {
                clientStatus.Enabled = false;
                try
                {
                    _context.Entry(clientStatus).CurrentValues.SetValues(clientStatus);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return new ClientDTO
            {
                Name = clientStatus.Name,
                CPF = clientStatus.CPF,
                IE = clientStatus.IE,
                ContributorType = clientStatus.ContributorType,
                Email = clientStatus.Email,
                Phone = clientStatus.Phone,
                Enabled = clientStatus.Enabled,
                Adress = new AdressDTO
                {
                    ZipCode = clientStatus.Adress.ZipCode,
                    Street = clientStatus.Adress.Street,
                    District = clientStatus.Adress.District,
                    County = clientStatus.Adress.County,
                    AdressNumber = clientStatus.Adress.AdressNumber,
                    UF = clientStatus.Adress.UF
                }
            };
        }
        public void DeleteClient(Guid id)
        {
            var client = _context.Clients
            .Where(c => c.Id == id)
            .Include(c => c.Adress).First();

            _context.Remove(client);
            _context.SaveChanges();

        }
    }
}