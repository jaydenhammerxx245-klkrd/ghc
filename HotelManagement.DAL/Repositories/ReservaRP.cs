using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DAL.Repositories
{
    public class ReservaRP : RepositorioBase<Reserva>, IReservaRepositorio
    {
        public ReservaRP(HotelDbContext context) : base(context)
        {
        }

        public Task<List<Reserva>> ObtenerPorHabitacionAsync(int idHabitacion)
        {
            return Set.AsNoTracking()
                .Where(r => r.IdHabitacion == idHabitacion)
                .ToListAsync();
        }

        public Task<List<Reserva>> ObtenerPorClienteAsync(int idCliente)
        {
            return Set.AsNoTracking()
                .Where(r => r.IdCliente == idCliente)
                .ToListAsync();
        }
    }
}
