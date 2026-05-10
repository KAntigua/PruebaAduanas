using Microsoft.EntityFrameworkCore;
using SistemaGestionAPI.Interfaces;
using SistemaGestionAPI.SistemaGestion.Entities;


namespace SistemaGestionAPI.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext context;

        public ProductoRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Producto>> GetAll()
        {
            return await context.Productos.ToListAsync();
        }

        public async Task<Producto> GetById(int id)
        {
            return await context.Productos
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(Producto producto)
        {
            context.Add(producto);
            await context.SaveChangesAsync();
        }

        public async Task Update(Producto producto)
        {
            var productoDB = await context.Productos
                .FirstOrDefaultAsync(x => x.Id == producto.Id);

            if (productoDB == null)
            {
                return;
            }

            productoDB.Nombre = producto.Nombre;
            productoDB.Descripcion = producto.Descripcion;
            productoDB.Precio = producto.Precio;
            productoDB.Stock = producto.Stock;

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var producto = await context.Productos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (producto == null)
            {
                return;
            }

            context.Productos.Remove(producto);

            await context.SaveChangesAsync();
        }
    }
}
