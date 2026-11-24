using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class ClienteBL : IClienteBL
    {
        private readonly IClienteRepositorio _repositorio;
        private readonly IReservaRepositorio _reservas;

        public ClienteBL(IClienteRepositorio repositorio, IReservaRepositorio reservas)
        {
            _repositorio = repositorio;
            _reservas = reservas;
        }

        public Task<List<Cliente>> GetAllAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Cliente?> GetByIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task AddAsync(Cliente cliente) => _repositorio.AgregarAsync(cliente);

        public Task<bool> UpdateAsync(Cliente cliente) => _repositorio.ActualizarAsync(cliente.IdCliente, cliente);

        /// <summary>
        /// Elimina un cliente. Regla de negocio: no se puede eliminar un cliente que tiene reservas,
        /// porque esas reservas quedarían huérfanas.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _repositorio.ObtenerPorIdAsync(id);
            if (cliente == null)
                return false;

            var reservas = await _reservas.ObtenerPorClienteAsync(id);
            if (reservas.Count > 0)
                throw new ReglaNegocioException(
                    $"No se puede eliminar al cliente {cliente.Nombre} {cliente.Apellido}: tiene {reservas.Count} reserva(s).");

            return await _repositorio.EliminarAsync(id);
        }
    }
}
