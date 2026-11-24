using HotelManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.IntegrationTests
{
    /// <summary>
    /// Demuestra que el refactor NO cambió el esquema de base de datos.
    /// Genera el script CREATE TABLE de SQL Server a partir del modelo actual (sin conectarse a ningún
    /// servidor) y lo compara con la instantánea generada desde el código anterior al refactor.
    /// </summary>
    public class CompatibilidadEsquemaTests
    {
        private static string GenerarEsquemaSqlServerActual()
        {
            var opciones = new DbContextOptionsBuilder<HotelDbContext>()
                .UseSqlServer("Server=.;Database=Crown") // no se abre la conexión
                .Options;

            using var contexto = new HotelDbContext(opciones);
            return Normalizar(contexto.Database.GenerateCreateScript());
        }

        private static string Normalizar(string sql) => sql.Replace("\r\n", "\n").Trim();

        [Fact]
        public void EsquemaSqlServer_EsIdenticoAlDeAntesDelRefactor()
        {
            var antes = Normalizar(File.ReadAllText(
                Path.Combine(AppContext.BaseDirectory, "Esquema", "EsquemaSqlServer_AntesDelRefactor.sql")));

            Assert.Equal(antes, GenerarEsquemaSqlServerActual());
        }

        [Fact]
        public void TablaReservas_ConservaSusColumnas()
        {
            var esquema = GenerarEsquemaSqlServerActual();

            Assert.Contains("CREATE TABLE [Reservas] (", esquema);
            Assert.Contains("[IdReserva] int NOT NULL IDENTITY", esquema);
            Assert.Contains("[IdCliente] int NOT NULL", esquema);
            Assert.Contains("[IdHabitacion] int NOT NULL", esquema);
            Assert.Contains("[FechaEntrada] datetime2 NOT NULL", esquema);
            Assert.Contains("[FechaSalida] datetime2 NOT NULL", esquema);
            Assert.Contains("[Total] decimal(18,2) NOT NULL", esquema);
            // Las reglas nuevas del dominio (Noches, SeSolapaCon...) no agregan columnas.
            Assert.DoesNotContain("[Noches]", esquema);
        }
    }
}
