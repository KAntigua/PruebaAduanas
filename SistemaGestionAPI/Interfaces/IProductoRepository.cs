using SistemaGestionAPI.SistemaGestion.Entities;

namespace SistemaGestionAPI.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAll();
        Task<Producto> GetById(int id);
        Task Add(Producto producto);

        Task Update(Producto producto);

        Task Delete(int id);
    }
}

