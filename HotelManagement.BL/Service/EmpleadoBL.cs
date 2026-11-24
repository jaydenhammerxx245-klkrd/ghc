using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class EmpleadoBL : IEmpleadoBL
    {
        private readonly IEmpleadoRepositorio _repositorio;

        public EmpleadoBL(IEmpleadoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<List<Empleado>> GetAllAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Empleado?> GetByIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task AddAsync(Empleado empleado) => _repositorio.AgregarAsync(empleado);

        public Task<bool> UpdateAsync(Empleado empleado) => _repositorio.ActualizarAsync(empleado.IdEmpleado, empleado);

        public Task<bool> DeleteAsync(int id) => _repositorio.EliminarAsync(id);
    }
}
