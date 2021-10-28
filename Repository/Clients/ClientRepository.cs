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
            return  _context.Clients
                .FirstOrDefault(c => c.Name == name);
        }
        public Client GetByIdTracking(Guid id)
        {
            return  _context.Clients
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
              return  _context.Clients
                .Include(c => c.Adress)
                .Select(c => new ClientDTO
                {
                    Name = c.Name,
                    CPF = c.CPF,
                    IE = c.IE,
                    ContributorType = c.ContributorType,
                    Email = c.Email,
                    Phone = c.Phone,
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
             var client =  GetByNameTracking(clientDto.Name);

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
                Name = clientDto.Name,
                CPF = clientDto.CPF,
                IE = clientDto.IE,
                ContributorType = clientDto.ContributorType,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
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
        public ClientDTO UpdateClient(ClientDTO clientDto)
        {
            var clientUpdate =  _context.Clients
            .Where(c => c.Id == clientDto.Guid)
            .Include(c => c.Adress)
            .ToList()
            .SingleOrDefault();

            if (clientUpdate != null)
            {
                // Update client
                _context.Entry(clientUpdate).CurrentValues.SetValues(clientDto);

                // Update and Insert adress
                foreach (var adress in _context.Adresses)
                {
                    var adressUpdate =  _context.Adresses
                        .Where(c => c.Id == clientDto.Adress.Guid)
                        .SingleOrDefault();

                    if (clientDto.Adress.Guid != null)
                    {
                        // Update adress
                        _context.Entry(adressUpdate).CurrentValues.SetValues(adress);
                    }
                    else
                    {   // add adress
                        var newAdress = new AdressDTO
                        {
                            ZipCode = adressUpdate.ZipCode,
                            Street = adressUpdate.Street,
                            District = adressUpdate.District,
                            County = adressUpdate.County,
                            AdressNumber = adressUpdate.AdressNumber,
                            UF = adressUpdate.UF
                        };
                        _context.Adresses.Add(adressUpdate);
                    }
                }
                 _context.SaveChanges();
                 
            }
            return new ClientDTO
            {
                Name = clientDto.Name,
                CPF = clientDto.CPF,
                IE = clientDto.IE,
                ContributorType = clientDto.ContributorType,
                Email = clientDto.Email,
                Phone = clientDto.Phone,
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
            var clientStatus =  _context.Clients.SingleOrDefault(c => c.Id.Equals(id));
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
            var client =  _context.Clients.OrderBy(c => c.Id == id).Include(c => c.Adress).First();
            _context.Remove(client);
            _context.SaveChanges();
        }
    }
}