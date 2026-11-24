using HotelManagement.BL.Repositorios;
using HotelManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.DAL.Repositories
{
    /// <summary>
    /// Implementación con Entity Framework del contrato IRepositorio que define la capa de negocio.
    /// Todo el detalle de persistencia (DbContext, SQL Server/SQLite) queda dentro de la DAL.
    /// </summary>
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected readonly HotelDbContext Context;
        protected readonly DbSet<T> Set;

        protected RepositorioBase(HotelDbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        public virtual Task<List<T>> ObtenerTodosAsync() => Set.AsNoTracking().ToListAsync();

        public virtual async Task<T?> ObtenerPorIdAsync(int id) => await Set.FindAsync(id);

        public virtual async Task AgregarAsync(T entidad)
        {
            await Set.AddAsync(entidad);
            await Context.SaveChangesAsync();
        }

        public virtual async Task<bool> ActualizarAsync(int id, T entidad)
        {
            var existente = await Set.FindAsync(id);
            if (existente == null)
                return false;

            // Copia los valores sobre la entidad rastreada (conserva la clave primaria).
            var entry = Context.Entry(existente);
            entry.CurrentValues.SetValues(entidad);
            foreach (var clave in entry.Metadata.FindPrimaryKey()!.Properties)
                entry.Property(clave.Name).IsModified = false;

            await Context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> EliminarAsync(int id)
        {
            var existente = await Set.FindAsync(id);
            if (existente == null)
                return false;

            Set.Remove(existente);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
