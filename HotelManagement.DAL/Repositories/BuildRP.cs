using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class BuildRP : RepositorioBase<Build>, IBuildRepositorio
    {
        public BuildRP(HotelDbContext context) : base(context)
        {
        }
    }
}
