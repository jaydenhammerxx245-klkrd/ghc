using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    public class BuildBL : IBuildBL
    {
        private readonly IBuildRepositorio _repositorio;

        public BuildBL(IBuildRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Task<List<Build>> ObtenerTodosAsync() => _repositorio.ObtenerTodosAsync();

        public Task<Build?> ObtenerPorIdAsync(int id) => _repositorio.ObtenerPorIdAsync(id);

        public Task CrearAsync(Build build) => _repositorio.AgregarAsync(build);

        public Task<bool> ActualizarAsync(Build build) => _repositorio.ActualizarAsync(build.IdBuild, build);

        public Task<bool> EliminarAsync(int id) => _repositorio.EliminarAsync(id);
    }
}
