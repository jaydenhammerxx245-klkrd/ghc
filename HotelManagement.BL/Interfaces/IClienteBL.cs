using HotelManagement.BL.Service;
using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IClienteBL
    {
        Task<List<Cliente>> GetAllAsync();

        Task<Cliente?> GetByIdAsync(int id);

        Task AddAsync(Cliente cliente);

        Task UpdateAsync(Cliente cliente);

        Task DeleteAsync(int id);
    }


}
