using Microsoft.EntityFrameworkCore;
using Client.Models;

namespace Client.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext (DbContextOptions<ClientContext> options)
            : base(options)
        {
        }

        public DbSet<mClient> Clients { get; set; }
        public DbSet<mWork> Works { get; set; }
    }
}