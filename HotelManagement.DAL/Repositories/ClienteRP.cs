using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class ClienteRP : RepositorioBase<Cliente>, IClienteRepositorio
    {
        public ClienteRP(HotelDbContext context) : base(context)
        {
        }
    }
}
