using HotelManagement.Entities;


namespace HotelManagement.DAL.Interfaces
{
    public interface IClienteDAL
    {
        Task<List<Cliente>> GetAll();

        Task<Cliente?> GetById(int id);

        Task Add(Cliente cliente);

        Task Update(Cliente cliente);

        Task Delete(int id);
    }
}