using HotelManagement.Entities;

namespace HotelManagement.BL.Repositorios
{
    public interface IReservaRepositorio : IRepositorio<Reserva>
    {
        /// <summary>Reservas existentes de una habitación (para validar solapamientos).</summary>
        Task<List<Reserva>> ObtenerPorHabitacionAsync(int idHabitacion);
    }
}
