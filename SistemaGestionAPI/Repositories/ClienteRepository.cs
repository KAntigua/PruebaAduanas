using Microsoft.EntityFrameworkCore;
using SistemaGestionAPI.Entities;
using SistemaGestionAPI.Interfaces;

namespace SistemaGestionAPI.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext context;

        public ClienteRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(Cliente cliente)
        {
            context.Add(cliente);

            await context.SaveChangesAsync();
        }

        public async Task Update(Cliente cliente)
        {
            var clienteDB = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == cliente.Id);

            if (clienteDB == null)
            {
                return;
            }

            clienteDB.Name = cliente.Name;
            clienteDB.Correo = cliente.Correo;
            clienteDB.Numero = cliente.Numero;

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cliente = await context.Clientes
                .FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return;
            }

            context.Clientes.Remove(cliente);

            await context.SaveChangesAsync();
        }
    }
}