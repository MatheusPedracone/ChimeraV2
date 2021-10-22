using System;
using System.Collections.Generic;
using System.Linq;
using Chimera_v2.Data;
using Chimera_v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Chimera_v2.Repository.Clients
{
    public class ClientRepository : IClientRepository
    {
        public List<Client> GetAllClient()
        {
            var context = new AppDbContext();
            List<Client> clients = context.Clients.Include(c => c.Adress).ToList();
            return clients;
        }
        public Client GetClient(Guid id)
        {
            throw new System.NotImplementedException();
        }
    }
}