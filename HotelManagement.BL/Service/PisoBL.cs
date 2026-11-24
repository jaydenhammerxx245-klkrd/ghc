using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class PisoBL : IPisoBL
    {
        private readonly IPisoDAL _pisoDAL;

        public PisoBL(IPisoDAL pisoDAL)
        {
            _pisoDAL = pisoDAL;
        }

        public async Task<List<Piso>> ObtenerTodosAsync()
        {
            return await _pisoDAL.ObtenerTodosAsync();
        }

        public async Task<Piso?> ObtenerPorIdAsync(int id)
        {
            return await _pisoDAL.ObtenerPorIdAsync(id);
        }

        public async Task CrearAsync(Piso piso)
        {
            await _pisoDAL.CrearAsync(piso);
        }

        public async Task ActualizarAsync(Piso piso)
        {
            await _pisoDAL.ActualizarAsync(piso);
        }

        public async Task EliminarAsync(int id)
        {
            await _pisoDAL.EliminarAsync(id);
        }
    }
}