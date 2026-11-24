using System.Text.RegularExpressions;

namespace HotelManagement.Tests
{
    /// <summary>
    /// Verifica que el refactor se mantenga: el negocio y el dominio no deben depender
    /// de la capa de datos ni de Entity Framework / SQL Server.
    /// Se comprueba de dos formas: en los ensamblados compilados y en los archivos de proyecto.
    /// </summary>
    public class ArquitecturaTests
    {
        private static readonly string Raiz = BuscarRaizDeLaSolucion();

        private static string BuscarRaizDeLaSolucion()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null && !dir.GetFiles("*.slnx").Any())
                dir = dir.Parent;
            return dir?.FullName ?? throw new InvalidOperationException("No se encontró la solución.");
        }

        [Theory]
        [InlineData(typeof(HotelManagement.BL.Services.ReservaBL))]
        [InlineData(typeof(HotelManagement.Entities.Reserva))]
        public void EnsambladoNoReferenciaPersistencia(Type tipoDeLaCapa)
        {
            var referencias = tipoDeLaCapa.Assembly.GetReferencedAssemblies()
                .Select(a => a.Name ?? string.Empty)
                .ToList();

            Assert.DoesNotContain("HotelManagement.DAL", referencias);
            Assert.DoesNotContain(referencias, r => r.StartsWith("Microsoft.EntityFrameworkCore"));
            Assert.DoesNotContain(referencias, r => r.StartsWith("Microsoft.Data.SqlClient"));
        }

        [Theory]
        [InlineData("HotelManagement.Entities")]
        [InlineData("HotelManagement.BL")]
        [InlineData("HotelManagement.Tests")]
        public void ProyectoNoDeclaraDependenciasDePersistencia(string proyecto)
        {
            var csproj = File.ReadAllText(Path.Combine(Raiz, proyecto, proyecto + ".csproj"));

            Assert.DoesNotContain("HotelManagement.DAL", csproj);
            Assert.DoesNotMatch(new Regex("EntityFrameworkCore|SqlClient|Sqlite"), csproj);
        }

        [Fact]
        public void ControladoresNoUsanLaBaseDeDatosDirectamente()
        {
            var carpetas = new[] { "Controllers", "Api" }
                .Select(c => Path.Combine(Raiz, "HotelApp.W", c));

            var infractores = carpetas
                .SelectMany(c => Directory.GetFiles(c, "*.cs"))
                .Where(f => Regex.IsMatch(File.ReadAllText(f), @"HotelDbContext|HotelManagement\.DAL"))
                .Select(Path.GetFileName)
                .ToList();

            Assert.Empty(infractores);
        }
    }
}
