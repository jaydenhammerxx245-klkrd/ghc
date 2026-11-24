using HotelManagement.BL.Interfaces;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class UsuarioBL : IUsuarioBL
    {
        private readonly IUsuarioDAL _usuarioDAL;

        public UsuarioBL(IUsuarioDAL usuarioDAL)
        {
            _usuarioDAL = usuarioDAL;
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await _usuarioDAL.ObtenerTodosAsync();
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _usuarioDAL.ObtenerPorIdAsync(id);
        }

        public async Task CrearAsync(Usuario usuario)
        {
            await _usuarioDAL.CrearAsync(usuario);
        }

        public async Task ActualizarAsync(Usuario usuario)
        {
            await _usuarioDAL.ActualizarAsync(usuario);
        }

        public async Task EliminarAsync(int id)
        {
            await _usuarioDAL.EliminarAsync(id);
        }
    }
}