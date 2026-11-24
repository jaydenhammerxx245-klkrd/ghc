using HotelManagement.DAL.Interfaces;
using HotelManagement.DAL.Context;
using HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DAL.Repositories
{
    public class BuildRP : IBuildDAL
    {
        private readonly HotelDbContext _context;

        public BuildRP(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<List<Build>> ObtenerTodosAsync()
        {
            return await _context.Builds.ToListAsync();
        }

        public async Task<Build?> ObtenerPorIdAsync(int id)
        {
            return await _context.Builds.FindAsync(id);
        }

        public async Task CrearAsync(Build build)
        {
            try
            {
                await _context.Builds.AddAsync(build);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el edificio.", ex);
            }
        }

        public async Task ActualizarAsync(Build build)
        {
            try
            {
                _context.Builds.Update(build);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el edificio.", ex);
            }
        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                var build = await _context.Builds.FindAsync(id);

                if (build != null)
                {
                    _context.Builds.Remove(build);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el edificio.", ex);
            }
        }
    }
}