using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Repositorios;
using HotelManagement.Entities;

namespace HotelManagement.BL.Services
{
    /// <summary>
    /// Flujo de reservas. Aplica las reglas de negocio y usa los repositorios solo
    /// a través de sus interfaces, por lo que puede probarse sin base de datos.
    /// </summary>
    public class ReservaBL : IReservaBL
    {
        private readonly IReservaRepositorio _reservas;
        private readonly IHabitacionRepositorio _habitaciones;
        private readonly IClienteRepositorio _clientes;

        public ReservaBL(
            IReservaRepositorio reservas,
            IHabitacionRepositorio habitaciones,
            IClienteRepositorio clientes)
        {
            _reservas = reservas;
            _habitaciones = habitaciones;
            _clientes = clientes;
        }

        public Task<List<Reserva>> ObtenerTodosAsync() => _reservas.ObtenerTodosAsync();

        public Task<Reserva?> ObtenerPorIdAsync(int id) => _reservas.ObtenerPorIdAsync(id);

        public async Task CrearAsync(Reserva reserva)
        {
            reserva.IdReserva = 0;
            await ValidarYCalcularAsync(reserva, exigirDisponible: true);
            await _reservas.AgregarAsync(reserva);
        }

        public async Task<bool> ActualizarAsync(Reserva reserva)
        {
            var existente = await _reservas.ObtenerPorIdAsync(reserva.IdReserva);
            if (existente == null)
                return false;

            // Una reserva ya hecha puede seguir editándose aunque su habitación se marque luego
            // como no disponible; la disponibilidad solo se exige si cambia de habitación.
            var cambiaDeHabitacion = existente.IdHabitacion != reserva.IdHabitacion;
            await ValidarYCalcularAsync(reserva, exigirDisponible: cambiaDeHabitacion);
            return await _reservas.ActualizarAsync(reserva.IdReserva, reserva);
        }

        public Task<bool> EliminarAsync(int id) => _reservas.EliminarAsync(id);

        /// <summary>
        /// Reglas de negocio de una reserva:
        /// 1. Cliente y habitación válidos y fecha de salida posterior a la de entrada.
        /// 2. El cliente debe existir.
        /// 3. La habitación debe existir y estar disponible (al crear o al cambiar de habitación).
        /// 4. La habitación no puede tener otra reserva que se cruce en fechas.
        /// 5. El total se calcula como noches × precio por noche (el precio debe ser mayor que cero).
        /// </summary>
        private async Task ValidarYCalcularAsync(Reserva reserva, bool exigirDisponible)
        {
            reserva.ValidarDatos();

            if (await _clientes.ObtenerPorIdAsync(reserva.IdCliente) == null)
                throw new ReglaNegocioException($"El cliente {reserva.IdCliente} no existe.");

            var habitacion = await _habitaciones.ObtenerPorIdAsync(reserva.IdHabitacion)
                ?? throw new ReglaNegocioException($"La habitación {reserva.IdHabitacion} no existe.");

            if (exigirDisponible && !habitacion.Disponible)
                throw new ReglaNegocioException($"La habitación {habitacion.Numero} no está disponible.");

            var existentes = await _reservas.ObtenerPorHabitacionAsync(reserva.IdHabitacion);
            if (existentes.Any(reserva.SeSolapaCon))
                throw new ReglaNegocioException(
                    $"La habitación {habitacion.Numero} ya está reservada en esas fechas.");

            reserva.CalcularTotal(habitacion);
        }
    }
}
