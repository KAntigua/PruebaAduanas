
using SistemaGestionAPI.Entities;

namespace SistemaGestionAPI.Interfaces
{
    public interface IVentaRepository
    {
        Task<List<Venta>> GetAll();

        Task<Venta> GetById(int id);

        Task Add(Venta venta);

        Task Update(Venta venta);

        Task Delete(int id);
    }
}