using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class HabitacionBL : IHabitacionBL
    {
        private readonly IHabitacionDAL _habitacionDAL;

        public HabitacionBL(IHabitacionDAL habitacionDAL)
        {
            _habitacionDAL = habitacionDAL;
        }

        public async Task<List<Habitacion>> GetAllAsync()
        {
            return await _habitacionDAL.GetAllAsync();
        }

        public async Task<Habitacion?> GetByIdAsync(int id)
        {
            return await _habitacionDAL.GetByIdAsync(id);
        }

        public async Task AddAsync(Habitacion habitacion)
        {
            await _habitacionDAL.AddAsync(habitacion);
        }

        public async Task UpdateAsync(Habitacion habitacion)
        {
            await _habitacionDAL.UpdateAsync(habitacion);
        }

        public async Task DeleteAsync(int id)
        {
            await _habitacionDAL.DeleteAsync(id);
        }
    }
}
