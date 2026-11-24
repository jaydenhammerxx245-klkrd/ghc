using HotelManagement.Entities;

namespace HotelManagement.Tests
{
    /// <summary>Reglas propias de la entidad Reserva (sin servicios ni repositorios).</summary>
    public class ReservaDominioTests
    {
        private static Reserva R(int id, int habitacion, string entrada, string salida) => new()
        {
            IdReserva = id,
            IdCliente = 1,
            IdHabitacion = habitacion,
            FechaEntrada = DateTime.Parse(entrada),
            FechaSalida = DateTime.Parse(salida),
        };

        [Fact]
        public void Noches_SeCalculanPorFecha()
        {
            Assert.Equal(2, R(0, 1, "2026-12-01 18:00", "2026-12-03 09:00").Noches);
        }

        [Theory]
        [InlineData("2026-12-02", "2026-12-03", 1, true)]  // dentro de otra
        [InlineData("2026-11-28", "2026-12-02", 1, true)]  // cruza el inicio
        [InlineData("2026-12-05", "2026-12-08", 1, false)] // empieza el día de salida
        [InlineData("2026-11-28", "2026-12-01", 1, false)] // termina el día de entrada
        [InlineData("2026-12-02", "2026-12-03", 2, false)] // otra habitación
        public void SeSolapaCon(string entrada, string salida, int habitacion, bool esperado)
        {
            var existente = R(1, 1, "2026-12-01", "2026-12-05");
            Assert.Equal(esperado, R(0, habitacion, entrada, salida).SeSolapaCon(existente));
        }

        [Fact]
        public void ValidarDatos_RechazaClienteInvalido()
        {
            var reserva = R(0, 1, "2026-12-01", "2026-12-02");
            reserva.IdCliente = 0;

            var ex = Assert.Throws<ReglaNegocioException>(reserva.ValidarDatos);
            Assert.Contains("cliente válido", ex.Message);
        }

        [Fact]
        public void ValidarDatos_RechazaHabitacionInvalida()
        {
            var reserva = R(0, 1, "2026-12-01", "2026-12-02");
            reserva.IdHabitacion = 0;

            var ex = Assert.Throws<ReglaNegocioException>(reserva.ValidarDatos);
            Assert.Contains("habitación válida", ex.Message);
        }

        [Fact]
        public void ValidarDatos_AceptaUnaReservaCorrecta()
        {
            var ex = Record.Exception(R(0, 1, "2026-12-01", "2026-12-02").ValidarDatos);
            Assert.Null(ex);
        }

        [Fact]
        public void SeSolapaCon_DetectaUnaReservaQueEnvuelveAOtra()
        {
            var existente = R(1, 1, "2026-12-03", "2026-12-04");
            Assert.True(R(0, 1, "2026-12-01", "2026-12-10").SeSolapaCon(existente));
        }

        [Fact]
        public void SeSolapaCon_IgnoraLaMismaReserva()
        {
            var reserva = R(7, 1, "2026-12-01", "2026-12-05");
            Assert.False(reserva.SeSolapaCon(R(7, 1, "2026-12-01", "2026-12-05")));
        }

        [Fact]
        public void CalcularTotal_UsaElPrecioPorNoche()
        {
            var reserva = R(0, 1, "2026-12-01", "2026-12-04");
            reserva.CalcularTotal(new Habitacion { PrecioNoche = 99.50m });
            Assert.Equal(298.50m, reserva.Total);
        }
    }
}
