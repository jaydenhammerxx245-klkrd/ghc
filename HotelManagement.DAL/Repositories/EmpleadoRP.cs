using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class EmpleadoRP : RepositorioBase<Empleado>, IEmpleadoRepositorio
    {
        public EmpleadoRP(HotelDbContext context) : base(context)
        {
        }
    }
}
