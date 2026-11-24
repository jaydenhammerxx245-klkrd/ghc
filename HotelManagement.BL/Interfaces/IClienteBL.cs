using HotelManagement.Entities;

namespace HotelManagement.BL.Interfaces
{
    public interface IClienteBL
    {
        Task<List<Cliente>> GetAllAsync();

        Task<Cliente?> GetByIdAsync(int id);

        Task AddAsync(Cliente cliente);

        Task<bool> UpdateAsync(Cliente cliente);

        Task<bool> DeleteAsync(int id);
    }


}
