using Microsoft.EntityFrameworkCore;

namespace BeerAPI.Db
{
    public class BeerDbContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; }

        public BeerDbContext(DbContextOptions<BeerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
