using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class EmpleadoRP : IEmpleadoDAL
    {
        private static readonly List<Empleado> empleados = [];

        public async Task<List<Empleado>> GetAllAsync()
        {
            return await Task.FromResult(empleados);
        }

        public async Task<Empleado?> GetByIdAsync(int id)
        {
            return await Task.FromResult(
                empleados.FirstOrDefault(e => e.IdEmpleado == id)
            );
        }

        public async Task AddAsync(Empleado empleado)
        {
            try
            {
                empleado.IdEmpleado = empleados.Count == 0 ? 1 : empleados.Max(e => e.IdEmpleado) + 1;

                empleados.Add(empleado);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el empleado.", ex);
            }
        }


        public async Task UpdateAsync(Empleado empleado)
        {
            try
            {
                var empleadoExistente = empleados
                    .FirstOrDefault(e => e.IdEmpleado == empleado.IdEmpleado);

                if (empleadoExistente != null)
                {
                    empleadoExistente.Nombre = empleado.Nombre;
                    empleadoExistente.Apellido = empleado.Apellido;
                    empleadoExistente.Telefono = empleado.Telefono;
                    empleadoExistente.Correo = empleado.Correo;
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el empleado.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var empleado = empleados
                    .FirstOrDefault(e => e.IdEmpleado == id);

                if (empleado != null)
                {
                    empleados.Remove(empleado);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el empleado.", ex);
            }
        }
    }
}