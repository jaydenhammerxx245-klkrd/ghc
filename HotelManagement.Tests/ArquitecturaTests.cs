namespace HotelManagement.Tests
{
    /// <summary>
    /// Verifica que el refactor se mantenga: el negocio y el dominio no deben depender
    /// de la capa de datos ni de Entity Framework / SQL Server.
    /// </summary>
    public class ArquitecturaTests
    {
        [Theory]
        [InlineData(typeof(HotelManagement.BL.Services.ReservaBL))]
        [InlineData(typeof(HotelManagement.Entities.Reserva))]
        public void CapaNoDependeDePersistencia(Type tipoDeLaCapa)
        {
            var referencias = tipoDeLaCapa.Assembly.GetReferencedAssemblies()
                .Select(a => a.Name ?? string.Empty)
                .ToList();

            Assert.DoesNotContain("HotelManagement.DAL", referencias);
            Assert.DoesNotContain(referencias, r => r.StartsWith("Microsoft.EntityFrameworkCore"));
            Assert.DoesNotContain(referencias, r => r.StartsWith("Microsoft.Data.SqlClient"));
        }
    }
}
