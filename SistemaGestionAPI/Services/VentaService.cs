using SistemaGestionAPI.Entities;
using SistemaGestionAPI.Interfaces;

namespace SistemaGestionAPI.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository repository;

        public VentaService(IVentaRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Venta>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<Venta> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task Add(Venta venta)
        {
            await repository.Add(venta);
        }

        public async Task Update(Venta venta)
        {
            await repository.Update(venta);
        }

        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}