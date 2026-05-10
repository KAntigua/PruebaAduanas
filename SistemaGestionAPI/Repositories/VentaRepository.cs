using Microsoft.EntityFrameworkCore;
using SistemaGestionAPI.Entities;
using SistemaGestionAPI.Interfaces;

namespace SistemaGestionAPI.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly ApplicationDbContext context;

        public VentaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Venta>> GetAll()
        {
            return await context.Ventas
                .Include(x => x.Cliente)
                .ToListAsync();
        }

        public async Task<Venta> GetById(int id)
        {
            return await context.Ventas
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(Venta venta)
        {
            context.Add(venta);

            await context.SaveChangesAsync();
        }

        public async Task Update(Venta venta)
        {
            var ventaDB = await context.Ventas
                .FirstOrDefaultAsync(x => x.Id == venta.Id);

            if (ventaDB == null)
            {
                return;
            }

            ventaDB.Fecha = venta.Fecha;
            ventaDB.ListaProductos = venta.ListaProductos;
            ventaDB.Total = venta.Total;
            ventaDB.ClienteId = venta.ClienteId;

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var venta = await context.Ventas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (venta == null)
            {
                return;
            }

            context.Ventas.Remove(venta);

            await context.SaveChangesAsync();
        }
    }
}