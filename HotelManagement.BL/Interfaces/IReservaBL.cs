using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IReservaBL
    {
        Task<List<Reserva>> ObtenerTodosAsync();
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Reserva reserva);
        Task<bool> ActualizarAsync(Reserva reserva);
        Task<bool> EliminarAsync(int id);
    }
}