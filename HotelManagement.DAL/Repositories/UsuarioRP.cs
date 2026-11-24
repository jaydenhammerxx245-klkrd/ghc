using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class UsuarioRP : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRP(HotelDbContext context) : base(context)
        {
        }
    }
}
