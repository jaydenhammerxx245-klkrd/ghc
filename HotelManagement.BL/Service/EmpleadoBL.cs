using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Service
{
    public class EmpleadoBL : IEmpleadoBL
    {
        private readonly IEmpleadoDAL _empleadoDAL;

        public EmpleadoBL(IEmpleadoDAL empleadoDAL)
        {
            _empleadoDAL = empleadoDAL;
        }

        public async Task<List<Empleado>> GetAllAsync()
        {
            return await _empleadoDAL.GetAllAsync();
        }

        public async Task<Empleado?> GetByIdAsync(int id)
        {
            return await _empleadoDAL.GetByIdAsync(id);
        }

        public async Task AddAsync(Empleado empleado)
        {
            await _empleadoDAL.AddAsync(empleado);
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            await _empleadoDAL.UpdateAsync(empleado);
        }

        public async Task DeleteAsync(int id)
        {
            await _empleadoDAL.DeleteAsync(id);
        }
    }
}
