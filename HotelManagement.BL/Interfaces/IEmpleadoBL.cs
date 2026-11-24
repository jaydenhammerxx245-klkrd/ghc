using HotelManagement.Entities;



namespace HotelManagement.BL.Interfaces
{
    public interface IEmpleadoBL
    {
        Task<List<Empleado>> GetAllAsync();
        Task<Empleado?> GetByIdAsync(int id);
        Task AddAsync(Empleado empleado);
        Task<bool> UpdateAsync(Empleado empleado);
        Task<bool> DeleteAsync(int id);
    }
}
