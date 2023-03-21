using CentralBank.Models;
using Microsoft.EntityFrameworkCore;

namespace CentralBank.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<ReferenceIndex> Indexes { get; set; }
    }
}
