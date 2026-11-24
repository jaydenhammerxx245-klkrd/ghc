using HotelManagement.Entities;

namespace HotelManagement.DAL.Interfaces
{
    public interface IBuildDAL
    {
        Task<List<Build>> ObtenerTodosAsync();
        Task<Build?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Build build);
        Task ActualizarAsync(Build build);
        Task EliminarAsync(int id);
    }
}