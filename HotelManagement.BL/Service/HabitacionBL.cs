using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class HabitacionBL : IHabitacionBL
    {
        private readonly IHabitacionRepositorio _repositorio;

        public HabitacionBL(IHabitacionRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<List<Habitacion>> GetAllAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Habitacion?> GetByIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task AddAsync(Habitacion habitacion) => _repositorio.AgregarAsync(habitacion);

        public Task<bool> UpdateAsync(Habitacion habitacion) => _repositorio.ActualizarAsync(habitacion.IdHabitacion, habitacion);

        public Task<bool> DeleteAsync(int id) => _repositorio.EliminarAsync(id);
    }
}
