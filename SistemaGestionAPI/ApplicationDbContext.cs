using Microsoft.EntityFrameworkCore;
using SistemaGestionAPI.Entities;
using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<VentaProducto> VentaProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VentaProducto>()
                .HasKey(vp => new { vp.VentaId, vp.ProductoId });
        }

    }
}
