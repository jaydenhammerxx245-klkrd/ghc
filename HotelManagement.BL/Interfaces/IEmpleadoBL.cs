using HotelManagement.BL.Service;
using HotelManagement.Entities;



namespace HotelManagement.BL.Interfaces
{
    public interface IEmpleadoBL
    {
        Task<List<Empleado>> GetAllAsync();
        Task<Empleado?> GetByIdAsync(int id);
        Task AddAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task DeleteAsync(int id);
    }
}
