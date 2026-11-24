using HotelManagement.Entities;
 

namespace HotelManagement.DAL.Interfaces
{
    public interface IHabitacionDAL
    {
        Task<List<Habitacion>> GetAllAsync();
        Task<Habitacion?> GetByIdAsync(int id);
        Task AddAsync(Habitacion habitacion);
        Task UpdateAsync(Habitacion habitacion);
        Task DeleteAsync(int id);
    }
}