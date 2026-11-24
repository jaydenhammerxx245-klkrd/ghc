using HotelManagement.DAL.Context;
using HotelManagement.DAL.Interfaces;
using HotelManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DAL.Repositories
{
    public class ClienteRP : IClienteDAL
    {
        private readonly HotelDbContext _context;

        public ClienteRP(HotelDbContext context)
        {
            _context = context;
        }

        public async Task Add(Cliente cliente)
        {
            try
            {
                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el cliente.", ex);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);

                if (cliente != null)
                {
                    _context.Clientes.Remove(cliente);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task Update(Cliente cliente)
        {
            try
            {
                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente.", ex);
            }
        }

    }
}
