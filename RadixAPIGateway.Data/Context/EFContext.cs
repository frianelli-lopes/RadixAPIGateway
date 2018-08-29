using Microsoft.EntityFrameworkCore;
using RadixAPIGateway.Domain.Models;

namespace RadixAPIGateway.Data.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options)
            : base(options)
        { }

        public DbSet<Store> Store { get; set; }
    }
}
