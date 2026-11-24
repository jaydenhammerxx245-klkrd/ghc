using HotelManagement.BL.Services;
using HotelManagement.DAL.Context;
using HotelManagement.DAL.Repositories;
using HotelManagement.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.IntegrationTests
{
    /// <summary>
    /// Flujo de reservas de punta a punta con los repositorios reales de la DAL
    /// sobre una base de datos SQLite en memoria creada con el mismo modelo (mismo esquema).
    /// </summary>
    public sealed class FlujoReservaConBaseDeDatosTests : IDisposable
    {
        private readonly SqliteConnection _conexion;
        private readonly HotelDbContext _contexto;
        private readonly ReservaBL _servicio;

        public FlujoReservaConBaseDeDatosTests()
        {
            _conexion = new SqliteConnection("Data Source=:memory:");
            _conexion.Open();
            _contexto = new HotelDbContext(new DbContextOptionsBuilder<HotelDbContext>().UseSqlite(_conexion).Options);
            _contexto.Database.EnsureCreated();

            _contexto.Clientes.Add(new Cliente { Nombre = "Luis", Apellido = "Paz" });
            _contexto.Habitaciones.Add(new Habitacion { Numero = "101", PrecioNoche = 80m, Disponible = true });
            _contexto.SaveChanges();
            _contexto.ChangeTracker.Clear();

            _servicio = new ReservaBL(new ReservaRP(_contexto), new HabitacionRP(_contexto), new ClienteRP(_contexto));
        }

        [Fact]
        public async Task ReservaValida_SeGuardaEnLaTablaReservas()
        {
            await _servicio.CrearAsync(new Reserva
            {
                IdCliente = 1,
                IdHabitacion = 1,
                FechaEntrada = new DateTime(2026, 12, 1),
                FechaSalida = new DateTime(2026, 12, 4),
            });

            _contexto.ChangeTracker.Clear();
            var guardada = Assert.Single(await _contexto.Reservas.ToListAsync());
            Assert.Equal(240m, guardada.Total);
        }

        [Fact]
        public async Task ReservaSolapada_SeRechaza_YNoSeGuarda()
        {
            await _servicio.CrearAsync(new Reserva
            {
                IdCliente = 1, IdHabitacion = 1,
                FechaEntrada = new DateTime(2026, 12, 1), FechaSalida = new DateTime(2026, 12, 4),
            });

            await Assert.ThrowsAsync<ReglaNegocioException>(() => _servicio.CrearAsync(new Reserva
            {
                IdCliente = 1, IdHabitacion = 1,
                FechaEntrada = new DateTime(2026, 12, 2), FechaSalida = new DateTime(2026, 12, 5),
            }));

            Assert.Equal(1, await _contexto.Reservas.CountAsync());
        }

        [Fact]
        public async Task Actualizar_YEliminar_UsanLosRepositoriosReales()
        {
            var reserva = new Reserva
            {
                IdCliente = 1, IdHabitacion = 1,
                FechaEntrada = new DateTime(2026, 12, 1), FechaSalida = new DateTime(2026, 12, 2),
            };
            await _servicio.CrearAsync(reserva);

            var cambio = new Reserva
            {
                IdReserva = reserva.IdReserva, IdCliente = 1, IdHabitacion = 1,
                FechaEntrada = new DateTime(2026, 12, 1), FechaSalida = new DateTime(2026, 12, 6),
            };
            Assert.True(await _servicio.ActualizarAsync(cambio));

            _contexto.ChangeTracker.Clear();
            Assert.Equal(400m, (await _contexto.Reservas.SingleAsync()).Total);

            Assert.True(await _servicio.EliminarAsync(reserva.IdReserva));
            Assert.False(await _servicio.EliminarAsync(reserva.IdReserva));
            Assert.Equal(0, await _contexto.Reservas.CountAsync());
        }

        public void Dispose()
        {
            _contexto.Dispose();
            _conexion.Dispose();
        }
    }
}
