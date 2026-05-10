using SistemaGestionAPI.Interfaces;
using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository repository;

        public ProductoService(IProductoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Producto>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<Producto> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task Add(Producto producto)
        {
            await repository.Add(producto);
        }

        public async Task Update(Producto producto)
        {
            await repository.Update(producto);
        }

        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}
