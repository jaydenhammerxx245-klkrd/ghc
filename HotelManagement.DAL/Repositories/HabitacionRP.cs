using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.DAL.Repositories
{
    public class HabitacionRP : IHabitacionDAL
    {
        private static readonly List<Habitacion> habitaciones = new();

        public async Task<List<Habitacion>> GetAllAsync()
        {
            try
            {
                return await Task.FromResult(habitaciones);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las habitaciones.", ex);
            }
        }

        public async Task<Habitacion?> GetByIdAsync(int id)
        {
            try
            {
                return await Task.FromResult(
                    habitaciones.FirstOrDefault(h => h.IdHabitacion == id)
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la habitación.", ex);
            }
        }


        public async Task AddAsync(Habitacion habitacion)
        {
            try
            {
                habitacion.IdHabitacion = habitaciones.Count == 0 ? 1 : habitaciones.Max(h => h.IdHabitacion) + 1;

                habitaciones.Add(habitacion);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la habitación.", ex);
            }
        }
        public async Task UpdateAsync(Habitacion habitacion)
        {
            try
            {
                var habitacionExistente = habitaciones
                    .FirstOrDefault(h => h.IdHabitacion == habitacion.IdHabitacion);

                if (habitacionExistente != null)
                {
                    habitacionExistente.Numero = habitacion.Numero;
                    habitacionExistente.Tipo = habitacion.Tipo;
                    habitacionExistente.Precio = habitacion.Precio;
                    habitacionExistente.Estado = habitacion.Estado;
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la habitación.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var habitacion = habitaciones
                    .FirstOrDefault(h => h.IdHabitacion == id);

                if (habitacion != null)
                {
                    habitaciones.Remove(habitacion);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la habitación.", ex);
            }
        }
    }
}