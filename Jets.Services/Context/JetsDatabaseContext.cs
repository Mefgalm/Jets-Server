using Jets.Database;
using System.Data.Entity;

namespace Jets.Services.Context
{
    public class JetsDatabaseContext : DbContext
    {
        public JetsDatabaseContext() : base("JetsDatabaseConnection")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
    }
}
