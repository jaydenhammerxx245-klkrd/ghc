using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class HabitacionBL : IHabitacionBL
    {
        private readonly IHabitacionRepositorio _repositorio;
        private readonly IReservaRepositorio _reservas;

        public HabitacionBL(IHabitacionRepositorio repositorio, IReservaRepositorio reservas)
        {
            _repositorio = repositorio;
            _reservas = reservas;
        }

        public Task<List<Habitacion>> GetAllAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Habitacion?> GetByIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task AddAsync(Habitacion habitacion) => _repositorio.AgregarAsync(habitacion);

        public Task<bool> UpdateAsync(Habitacion habitacion) => _repositorio.ActualizarAsync(habitacion.IdHabitacion, habitacion);

        /// <summary>
        /// Elimina una habitación. Regla de negocio: no se puede eliminar una habitación que tiene reservas.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var habitacion = await _repositorio.ObtenerPorIdAsync(id);
            if (habitacion == null)
                return false;

            var reservas = await _reservas.ObtenerPorHabitacionAsync(id);
            if (reservas.Count > 0)
                throw new ReglaNegocioException(
                    $"No se puede eliminar la habitación {habitacion.Numero}: tiene {reservas.Count} reserva(s).");

            return await _repositorio.EliminarAsync(id);
        }
    }
}
