using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class HabitacionRP : RepositorioBase<Habitacion>, IHabitacionRepositorio
    {
        public HabitacionRP(HotelDbContext context) : base(context)
        {
        }
    }
}
