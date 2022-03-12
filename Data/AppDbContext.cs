using Microsoft.EntityFrameworkCore;
using PlatformServices.Models;

namespace PlatformServices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
        {
            
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}