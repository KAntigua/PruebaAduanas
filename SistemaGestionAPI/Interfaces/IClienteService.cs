using SistemaGestionAPI.Entities;

namespace SistemaGestionAPI.Interfaces
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAll();

        Task<Cliente> GetById(int id);

        Task Add(Cliente cliente);

        Task Update(Cliente cliente);

        Task Delete(int id);
    }
}