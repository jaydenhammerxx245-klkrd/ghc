using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class UsuarioBL : IUsuarioBL
    {
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioBL(IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<List<Usuario>> ObtenerTodosAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Usuario?> ObtenerPorIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task CrearAsync(Usuario usuario) => _repositorio.AgregarAsync(usuario);

        public Task<bool> ActualizarAsync(Usuario usuario) => _repositorio.ActualizarAsync(usuario.IdUsuario, usuario);

        public Task<bool> EliminarAsync(int id) => _repositorio.EliminarAsync(id);
    }
}
