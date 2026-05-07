using Microsoft.EntityFrameworkCore;
using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
    }
}
