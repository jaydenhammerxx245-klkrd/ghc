using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Service
{
    public class ClienteBL : IClienteBL
    {
        private static readonly List<Cliente> clientes = [];
        private IClienteDAL _ClienteDAL;

        public ClienteBL(IClienteDAL clienteDAL)
        {
            _ClienteDAL = clienteDAL;
        }
        public async Task<List<Cliente>> GetAllAsync()
        {
            return await Task.FromResult(clientes);
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await Task.FromResult(
                clientes.FirstOrDefault(c => c.IdCliente == id)
            );
        }

        public async Task AddAsync(Cliente cliente)
        {
            cliente.IdCliente = clientes.Count + 1;

            clientes.Add(cliente);

            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            var clienteActual = clientes.FirstOrDefault(
                c => c.IdCliente == cliente.IdCliente);

            if (clienteActual != null)
            {
                clienteActual.Nombre = cliente.Nombre;
                clienteActual.Apellido = cliente.Apellido;
                clienteActual.Cedula = cliente.Cedula;
                clienteActual.Telefono = cliente.Telefono;
                clienteActual.Correo = cliente.Correo;
            }

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = clientes.FirstOrDefault(
                c => c.IdCliente == id);

            if (cliente != null)
            {
                clientes.Remove(cliente);
            }

            await Task.CompletedTask;
        }

    }
}
