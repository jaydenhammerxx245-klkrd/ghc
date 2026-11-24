using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IHabitacionBL
    {
        Task<List<Habitacion>> GetAllAsync();
        Task<Habitacion?> GetByIdAsync(int id);
        Task AddAsync(Habitacion habitacion);
        Task UpdateAsync(Habitacion habitacion);
        Task DeleteAsync(int id);
    }
}