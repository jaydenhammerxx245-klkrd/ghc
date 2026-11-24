using HotelManagement.Entities;

namespace HotelManagement.BL.Repositorios
{
    public interface IReservaRepositorio : IRepositorio<Reserva>
    {
        /// <summary>Reservas existentes de una habitación (para validar solapamientos).</summary>
        Task<List<Reserva>> ObtenerPorHabitacionAsync(int idHabitacion);

        /// <summary>Reservas de un cliente (para impedir borrar clientes con reservas).</summary>
        Task<List<Reserva>> ObtenerPorClienteAsync(int idCliente);
    }
}
