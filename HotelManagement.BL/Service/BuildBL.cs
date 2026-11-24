using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class BuildBL : IBuildBL
    {
        private readonly IBuildDAL _buildDAL;

        public BuildBL(IBuildDAL buildDAL)
        {
            _buildDAL = buildDAL;
        }

        public async Task<List<Build>> ObtenerTodosAsync()
        {
            return await _buildDAL.ObtenerTodosAsync();
        }

        public async Task<Build?> ObtenerPorIdAsync(int id)
        {
            return await _buildDAL.ObtenerPorIdAsync(id);
        }

        public async Task CrearAsync(Build build)
        {
            await _buildDAL.CrearAsync(build);
        }

        public async Task ActualizarAsync(Build build)
        {
            await _buildDAL.ActualizarAsync(build);
        }

        public async Task EliminarAsync(int id)
        {
            await _buildDAL.EliminarAsync(id);
        }
    }
}