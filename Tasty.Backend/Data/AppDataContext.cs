using Microsoft.EntityFrameworkCore;
using Tasty.Backend.Models;

namespace Tasty.Backend.Data
{
        public class AppDataContext : DbContext
        {
            public AppDataContext(DbContextOptions<AppDataContext> options)
                : base(options)
            {
            }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
    }
}
