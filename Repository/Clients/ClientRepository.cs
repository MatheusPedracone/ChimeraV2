using System;
using System.Collections.Generic;
using System.Linq;
using Chimera_v2.Data;
using Chimera_v2.DTOs;
using Chimera_v2.Models;
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

        public List<ClientDTO> GetAllClients()
        {
        }

        public ClientDTO GetClient(Guid id)
        {
            var client = _context.Clients
                                       .Include(c => c.Adress)
                                       .Where(c => c.Id == id)
                                       .FirstOrDefault();
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

    }
}
