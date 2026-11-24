using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IPisoBL
    {
        Task<List<Piso>> ObtenerTodosAsync();
        Task<Piso?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Piso piso);
        Task<bool> ActualizarAsync(Piso piso);
        Task<bool> EliminarAsync(int id);
    }
}