using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IHabitacionBL
    {
        Task<List<Habitacion>> GetAllAsync();
        Task<Habitacion?> GetByIdAsync(int id);
        Task AddAsync(Habitacion habitacion);
        Task<bool> UpdateAsync(Habitacion habitacion);
        Task<bool> DeleteAsync(int id);
    }
}