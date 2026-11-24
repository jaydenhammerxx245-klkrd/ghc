using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class PisoBL : IPisoBL
    {
        private readonly IPisoRepositorio _repositorio;

        public PisoBL(IPisoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<List<Piso>> ObtenerTodosAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Piso?> ObtenerPorIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task CrearAsync(Piso piso) => _repositorio.AgregarAsync(piso);

        public Task<bool> ActualizarAsync(Piso piso) => _repositorio.ActualizarAsync(piso.IdPiso, piso);

        public Task<bool> EliminarAsync(int id) => _repositorio.EliminarAsync(id);
    }
}
