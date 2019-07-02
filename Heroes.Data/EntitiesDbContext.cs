using Heroes.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Data
{
    public class EntitiesDbContext : DbContext
    {
        public EntitiesDbContext(DbContextOptions<EntitiesDbContext> options)
            : base(options)
        {
        }

         public DbSet<HeroEntity> Heroes { get; set; }
    }
}