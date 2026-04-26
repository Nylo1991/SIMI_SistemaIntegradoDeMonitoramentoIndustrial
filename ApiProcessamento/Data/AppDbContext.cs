using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiProcessamento.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SensorData> Sensores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}