using HotelManagement.DAL.Context;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DAL.Repositories
{
    public class PisoRP : IPisoDAL
    {
        private readonly HotelDbContext _context;

        public PisoRP(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<List<Piso>> ObtenerTodosAsync()
        {
            try
            {
                return await _context.Pisos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pisos.", ex);
            }
        }

        public async Task<Piso?> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _context.Pisos.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el piso.", ex);
            }
        }

        public async Task CrearAsync(Piso piso)
        {
            try
            {
                await _context.Pisos.AddAsync(piso);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el piso.", ex);
            }
        }

        public async Task ActualizarAsync(Piso piso)
        {
            try
            {
                _context.Pisos.Update(piso);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el piso.", ex);
            }
        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                var piso = await _context.Pisos.FindAsync(id);

                if (piso != null)
                {
                    _context.Pisos.Remove(piso);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el piso.", ex);
            }
        }
    }
}