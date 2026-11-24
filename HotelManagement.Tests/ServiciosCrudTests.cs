using HotelManagement.BL.Services;
using HotelManagement.Entities;
using HotelManagement.Tests.Fakes;

namespace HotelManagement.Tests
{
    /// <summary>
    /// Servicios de Cliente y Habitación: CRUD a través del repositorio (inyectado como interfaz)
    /// y la regla que impide eliminar registros con reservas. Sin base de datos.
    /// </summary>
    public class ServiciosCrudTests
    {
        private readonly ClienteRepositorioFalso _clientes = new();
        private readonly HabitacionRepositorioFalso _habitaciones = new();
        private readonly ReservaRepositorioFalso _reservas = new();

        [Fact]
        public async Task ClienteBL_CrudCompleto()
        {
            var servicio = new ClienteBL(_clientes, _reservas);

            await servicio.AddAsync(new Cliente { Nombre = "Ana", Apellido = "Gil" });
            var cliente = Assert.Single(await servicio.GetAllAsync());
            Assert.Equal(1, cliente.IdCliente);

            Assert.True(await servicio.UpdateAsync(new Cliente { IdCliente = 1, Nombre = "Ana María", Apellido = "Gil" }));
            Assert.Equal("Ana María", (await servicio.GetByIdAsync(1))!.Nombre);

            Assert.False(await servicio.UpdateAsync(new Cliente { IdCliente = 99 }));
            Assert.True(await servicio.DeleteAsync(1));
            Assert.False(await servicio.DeleteAsync(1));
            Assert.Null(await servicio.GetByIdAsync(1));
        }

        [Fact]
        public async Task ClienteConReservas_NoSePuedeEliminar()
        {
            var servicio = new ClienteBL(_clientes, _reservas);
            await servicio.AddAsync(new Cliente { Nombre = "Ana", Apellido = "Gil" });
            await _reservas.AgregarAsync(new Reserva { IdCliente = 1, IdHabitacion = 1 });

            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(() => servicio.DeleteAsync(1));

            Assert.Contains("tiene 1 reserva", ex.Message);
            Assert.NotNull(await servicio.GetByIdAsync(1));
        }

        [Fact]
        public async Task HabitacionBL_CrudCompleto()
        {
            var servicio = new HabitacionBL(_habitaciones, _reservas);

            await servicio.AddAsync(new Habitacion { Numero = "201", PrecioNoche = 90m, Disponible = true });
            await servicio.AddAsync(new Habitacion { Numero = "202", PrecioNoche = 95m, Disponible = true });
            Assert.Equal(2, (await servicio.GetAllAsync()).Count);

            Assert.True(await servicio.UpdateAsync(new Habitacion { IdHabitacion = 2, Numero = "202", PrecioNoche = 110m }));
            Assert.Equal(110m, (await servicio.GetByIdAsync(2))!.PrecioNoche);

            Assert.False(await servicio.UpdateAsync(new Habitacion { IdHabitacion = 99 }));
            Assert.True(await servicio.DeleteAsync(1));
            Assert.False(await servicio.DeleteAsync(99));
        }

        [Fact]
        public async Task HabitacionConReservas_NoSePuedeEliminar()
        {
            var servicio = new HabitacionBL(_habitaciones, _reservas);
            await servicio.AddAsync(new Habitacion { Numero = "201", PrecioNoche = 90m, Disponible = true });
            await _reservas.AgregarAsync(new Reserva { IdCliente = 1, IdHabitacion = 1 });

            var ex = await Assert.ThrowsAsync<ReglaNegocioException>(() => servicio.DeleteAsync(1));

            Assert.Contains("habitación 201", ex.Message);
            Assert.NotNull(await servicio.GetByIdAsync(1));
        }
    }
}
