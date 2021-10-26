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

        public async Task<ClientDTO> GetClientAsync(Guid id)
        {
            var client = await _context.Clients
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
                .FirstOrDefaultAsync();

            return client ?? null;
        }

        public async Task<List<ClientDTO>> GetAllClientsAsync()
        {
            return await _context.Clients
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
                .ToListAsync();
        }
        public async Task<ClientDTO> CreateClient(ClientDTO clientDto)
        {
            var client = await GetByNameTracking(clientDto.Name);

            if (client != default)
            {
                throw new BadHttpRequestException("Client already existis!");
            }
            await _context.Clients.AddAsync(new Client

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
            await _context.SaveChangesAsync();
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
        public async Task<ClientDTO> UpdateClient(ClientDTO clientDto)
        {
            var client = await GetByNameTracking(clientDto.Name);

            if (client == default)
            {
                return default;
            }

            client.Name = clientDto.Name;
            client.CPF = clientDto.CPF;
            client.IE = clientDto.IE;
            client.ContributorType = clientDto.ContributorType;
            client.Email = clientDto.Email;
            client.Phone = clientDto.Phone;
            client.Adress = new Adress
            {
                ZipCode = clientDto.Adress.ZipCode,
                Street = clientDto.Adress.Street,
                District = clientDto.Adress.District,
                County = clientDto.Adress.County,
                AdressNumber = clientDto.Adress.AdressNumber,
                UF = clientDto.Adress.UF
            };
            await _context.SaveChangesAsync();

            return new ClientDTO()
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
        public async Task<Client> GetByNameTracking(string name)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Client> GetByIdTracking(Guid id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task DeleteClient(Guid id)
        {
            var client = await GetByIdTracking(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}