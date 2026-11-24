using HotelManagement.Entities;

namespace HotelManagement.DAL.Interfaces
{
    public interface IEmpleadoDAL
    {
        Task<List<Empleado>> GetAllAsync();

        Task<Empleado?> GetByIdAsync(int id);

        Task AddAsync(Empleado empleado);

        Task UpdateAsync(Empleado empleado);

        Task DeleteAsync(int id);
    }
}
