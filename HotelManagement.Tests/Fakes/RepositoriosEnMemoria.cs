using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.Tests.Fakes
{
    /// <summary>
    /// Repositorio falso en memoria: sustituye a la base de datos en las pruebas.
    /// Es posible porque la capa de negocio solo conoce la interfaz IRepositorio.
    /// </summary>
    public class RepositorioEnMemoria<T> : IRepositorio<T> where T : class
    {
        protected readonly List<T> Datos = [];
        private readonly Func<T, int> _obtenerId;
        private readonly Action<T, int> _asignarId;

        public RepositorioEnMemoria(Func<T, int> obtenerId, Action<T, int> asignarId)
        {
            _obtenerId = obtenerId;
            _asignarId = asignarId;
        }

        public int Guardados => Datos.Count;

        public Task<List<T>> ObtenerTodosAsync() => Task.FromResult(Datos.ToList());

        public Task<T?> ObtenerPorIdAsync(int id) =>
            Task.FromResult(Datos.FirstOrDefault(d => _obtenerId(d) == id));

        public Task AgregarAsync(T entidad)
        {
            _asignarId(entidad, Datos.Count == 0 ? 1 : Datos.Max(_obtenerId) + 1);
            Datos.Add(entidad);
            return Task.CompletedTask;
        }

        public Task<bool> ActualizarAsync(int id, T entidad)
        {
            var indice = Datos.FindIndex(d => _obtenerId(d) == id);
            if (indice < 0)
                return Task.FromResult(false);

            Datos[indice] = entidad;
            return Task.FromResult(true);
        }

        public Task<bool> EliminarAsync(int id) =>
            Task.FromResult(Datos.RemoveAll(d => _obtenerId(d) == id) > 0);
    }

    public class ClienteRepositorioFalso()
        : RepositorioEnMemoria<Cliente>(c => c.IdCliente, (c, id) => c.IdCliente = id), IClienteRepositorio;

    public class HabitacionRepositorioFalso()
        : RepositorioEnMemoria<Habitacion>(h => h.IdHabitacion, (h, id) => h.IdHabitacion = id), IHabitacionRepositorio;

    public class ReservaRepositorioFalso()
        : RepositorioEnMemoria<Reserva>(r => r.IdReserva, (r, id) => r.IdReserva = id), IReservaRepositorio
    {
        public Task<List<Reserva>> ObtenerPorHabitacionAsync(int idHabitacion) =>
            Task.FromResult(Datos.Where(r => r.IdHabitacion == idHabitacion).ToList());
    }
}
