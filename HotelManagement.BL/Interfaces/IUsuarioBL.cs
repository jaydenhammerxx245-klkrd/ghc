using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IUsuarioBL
    {
        Task<List<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Usuario usuario);
        Task<bool> ActualizarAsync(Usuario usuario);
        Task<bool> EliminarAsync(int id);
    }
}