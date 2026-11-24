using HotelManagement.DAL.Context;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DAL.Repositories
{
    public class ReservaRP : IReservaDAL
    {
        private readonly HotelDbContext _context;

        public ReservaRP(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> ObtenerTodosAsync()
        {
            try
            {
                return await _context.Reservas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las reservas.", ex);
            }
        }

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _context.Reservas.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la reserva.", ex);
            }
        }
        public async Task CrearAsync(Reserva reserva)
        {
            try
            {
                await _context.Reservas.AddAsync(reserva);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la reserva.", ex);
            }
        }

        public async Task ActualizarAsync(Reserva reserva)
        {
            try
            {
                _context.Reservas.Update(reserva);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reserva.", ex);
            }
        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                var reserva = await _context.Reservas.FindAsync(id);

                if (reserva != null)
                {
                    _context.Reservas.Remove(reserva);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reserva.", ex);
            }
        }
    }
}