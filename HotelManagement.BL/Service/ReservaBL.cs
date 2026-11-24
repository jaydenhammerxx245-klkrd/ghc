using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class ReservaBL : IReservaBL
    {
        private readonly IReservaDAL _reservaDAL;

        public ReservaBL(IReservaDAL reservaDAL)
        {
            _reservaDAL = reservaDAL;
        }

        public async Task<List<Reserva>> ObtenerTodosAsync()
        {
            return await _reservaDAL.ObtenerTodosAsync();
        }

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            return await _reservaDAL.ObtenerPorIdAsync(id);
        }

        public async Task CrearAsync(Reserva reserva)
        {
            await _reservaDAL.CrearAsync(reserva);
        }

        public async Task ActualizarAsync(Reserva reserva)
        {
            await _reservaDAL.ActualizarAsync(reserva);
        }

        public async Task EliminarAsync(int id)
        {
            await _reservaDAL.EliminarAsync(id);
        }
    }
}    
