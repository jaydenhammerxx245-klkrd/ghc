using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class ClienteBL : IClienteBL
    {
        private readonly IClienteRepositorio _repositorio;

        public ClienteBL(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<List<Cliente>> GetAllAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Cliente?> GetByIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task AddAsync(Cliente cliente) => _repositorio.AgregarAsync(cliente);

        public Task<bool> UpdateAsync(Cliente cliente) => _repositorio.ActualizarAsync(cliente.IdCliente, cliente);

        public Task<bool> DeleteAsync(int id) => _repositorio.EliminarAsync(id);
    }
}
