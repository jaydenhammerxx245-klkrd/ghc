using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class PisoRP : RepositorioBase<Piso>, IPisoRepositorio
    {
        public PisoRP(HotelDbContext context) : base(context)
        {
        }
    }
}
