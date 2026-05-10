using SistemaGestionAPI.Entities;
using SistemaGestionAPI.Interfaces;

namespace SistemaGestionAPI.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository repository;

        public ClienteService(IClienteRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task Add(Cliente cliente)
        {
            await repository.Add(cliente);
        }

        public async Task Update(Cliente cliente)
        {
            await repository.Update(cliente);
        }

        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}