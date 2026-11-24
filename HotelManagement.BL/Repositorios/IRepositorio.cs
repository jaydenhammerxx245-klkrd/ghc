namespace HotelManagement.BL.Repositorios
{
    /// <summary>
    /// Contrato genérico de persistencia que necesita la capa de negocio.
    /// La capa de negocio lo define y la capa de datos (DAL) lo implementa,
    /// así el negocio no depende de Entity Framework ni de SQL Server.
    /// </summary>
    public interface IRepositorio<T> where T : class
    {
        Task<List<T>> ObtenerTodosAsync();

        Task<T?> ObtenerPorIdAsync(int id);

        Task AgregarAsync(T entidad);

        /// <returns>false si el registro no existe.</returns>
        Task<bool> ActualizarAsync(int id, T entidad);

        /// <returns>false si el registro no existe.</returns>
        Task<bool> EliminarAsync(int id);
    }
}
