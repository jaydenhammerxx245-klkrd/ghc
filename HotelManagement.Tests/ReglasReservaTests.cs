using HotelManagement.BL.Services;
using HotelManagement.Entities;
using HotelManagement.Tests.Fakes;

namespace HotelManagement.Tests
{
    /// <summary>
    /// Pruebas de las reglas de negocio del flujo de reservas.
    /// Ninguna usa base de datos: este proyecto ni siquiera referencia la DAL ni Entity Framework.
    /// </summary>
    public class ReglasReservaTests
    {
        private readonly ClienteRepositorioFalso _clientes = new();
        private readonly HabitacionRepositorioFalso _habitaciones = new();
        private readonly ReservaRepositorioFalso _reservas = new();
        private readonly ReservaBL _servicio;

        public ReglasReservaTests()
        {
            _clientes.AgregarAsync(new Cliente { Nombre = "Luis", Apellido = "Paz" }).Wait();                              // Id 1
            _habitaciones.AgregarAsync(new Habitacion { Numero = "101", PrecioNoche = 80m, Disponible = true }).Wait();   // Id 1
            _habitaciones.AgregarAsync(new Habitacion { Numero = "102", PrecioNoche = 120m, Disponible = false }).Wait(); // Id 2
            _servicio = new ReservaBL(_reservas, _habitaciones, _clientes);
        }

        private static Reserva NuevaReserva(int habitacion = 1, string entrada = "2026-12-01", string salida = "2026-12-04", int cliente = 1) => new()
        {
            IdCliente = cliente,
            IdHabitacion = habitacion,
            FechaEntrada = DateTime.Parse(entrada),
            FechaSalida = DateTime.Parse(salida),
        };

        [Fact]
        public async Task ReservaValida_SeGuarda_YCalculaElTotal()
        {
            var reserva = NuevaReserva();

            await _servicio.CrearAsync(reserva);

            Assert.Equal(1, _reservas.Guardados);
            Assert.Equal(1, reserva.IdReserva);
            Assert.Equal(3, reserva.Noches);
            Assert.Equal(240m, reserva.Total); // 3 noches × 80
        }

        [Fact]
        public async Task ElTotalEnviadoPorElCliente_SeIgnora()
        {
            var reserva = NuevaReserva();
            reserva.Total = -5m;

            await _servicio.CrearAsync(reserva);

            Assert.Equal(240m, reserva.Total);
        }

        [Theory]
        [InlineData("2026-12-05", "2026-12-01")] // salida antes de la entrada
        [InlineData("2026-12-05", "2026-12-05")] // cero noches
        public async Task FechasInvalidas_SeRechazan(string entrada, string salida)
        {
            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(
                () => _servicio.CrearAsync(NuevaReserva(entrada: entrada, salida: salida)));

            Assert.Contains("fecha de salida", ex.Message);
            Assert.Equal(0, _reservas.Guardados);
        }

        [Fact]
        public async Task ClienteInexistente_SeRechaza()
        {
            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(
                () => _servicio.CrearAsync(NuevaReserva(cliente: 99)));

            Assert.Contains("cliente 99 no existe", ex.Message);
            Assert.Equal(0, _reservas.Guardados);
        }

        [Fact]
        public async Task HabitacionInexistente_SeRechaza()
        {
            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(
                () => _servicio.CrearAsync(NuevaReserva(habitacion: 99)));

            Assert.Contains("habitación 99 no existe", ex.Message);
            Assert.Equal(0, _reservas.Guardados);
        }

        [Fact]
        public async Task HabitacionNoDisponible_SeRechaza()
        {
            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(
                () => _servicio.CrearAsync(NuevaReserva(habitacion: 2)));

            Assert.Contains("no está disponible", ex.Message);
            Assert.Equal(0, _reservas.Guardados);
        }

        [Fact]
        public async Task ReservaQueSeSolapa_SeRechaza()
        {
            await _servicio.CrearAsync(NuevaReserva(entrada: "2026-12-01", salida: "2026-12-04"));

            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(
                () => _servicio.CrearAsync(NuevaReserva(entrada: "2026-12-03", salida: "2026-12-06")));

            Assert.Contains("ya está reservada", ex.Message);
            Assert.Equal(1, _reservas.Guardados);
        }

        [Fact]
        public async Task ReservaQueEmpiezaElDiaDeSalidaDeOtra_SePermite()
        {
            await _servicio.CrearAsync(NuevaReserva(entrada: "2026-12-01", salida: "2026-12-04"));

            await _servicio.CrearAsync(NuevaReserva(entrada: "2026-12-04", salida: "2026-12-06"));

            Assert.Equal(2, _reservas.Guardados);
        }

        [Fact]
        public async Task ActualizarUnaReserva_NoChocaConsigoMisma_YRecalculaTotal()
        {
            var reserva = NuevaReserva(entrada: "2026-12-01", salida: "2026-12-04");
            await _servicio.CrearAsync(reserva);

            var cambio = NuevaReserva(entrada: "2026-12-02", salida: "2026-12-07");
            cambio.IdReserva = reserva.IdReserva;
            var actualizado = await _servicio.ActualizarAsync(cambio);

            Assert.True(actualizado);
            Assert.Equal(400m, cambio.Total); // 5 noches × 80
        }

        [Fact]
        public async Task ActualizarReservaInexistente_DevuelveFalse()
        {
            var cambio = NuevaReserva();
            cambio.IdReserva = 42;

            Assert.False(await _servicio.ActualizarAsync(cambio));
        }
    }
}
