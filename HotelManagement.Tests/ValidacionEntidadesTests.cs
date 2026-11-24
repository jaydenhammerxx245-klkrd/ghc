using System.ComponentModel.DataAnnotations;
using HotelManagement.Entities;

namespace HotelManagement.Tests
{
    /// <summary>
    /// Validaciones declaradas en las entidades del dominio (las mismas que aplica la API y la web
    /// antes de llamar al negocio). Se evalúan en memoria con el validador estándar de .NET.
    /// </summary>
    public class ValidacionEntidadesTests
    {
        private static List<string> Errores(object entidad)
        {
            var resultados = new List<ValidationResult>();
            Validator.TryValidateObject(entidad, new ValidationContext(entidad), resultados, validateAllProperties: true);
            return resultados.Select(r => r.ErrorMessage ?? string.Empty).ToList();
        }

        [Fact]
        public void ClienteVacio_NoEsValido()
        {
            var errores = Errores(new Cliente());

            Assert.Contains("El nombre es obligatorio.", errores);
            Assert.Contains("El correo es obligatorio.", errores);
            Assert.Equal(5, errores.Count);
        }

        [Fact]
        public void ClienteConCorreoInvalido_NoEsValido()
        {
            var cliente = new Cliente { Nombre = "A", Apellido = "B", Cedula = "1", Telefono = "2", Correo = "sin-arroba" };
            Assert.Equal(["El correo no tiene un formato válido."], Errores(cliente));
        }

        [Fact]
        public void ClienteCompleto_EsValido()
        {
            var cliente = new Cliente { Nombre = "A", Apellido = "B", Cedula = "1", Telefono = "2", Correo = "a@b.com" };
            Assert.Empty(Errores(cliente));
        }

        [Theory]
        [InlineData(0, 80, 1, "La capacidad debe estar entre 1 y 50 personas.")]
        [InlineData(2, 0, 1, "El precio por noche debe ser mayor que cero.")]
        [InlineData(2, -80, 1, "El precio por noche debe ser mayor que cero.")]
        [InlineData(2, 80, 0, "Debe indicar el piso.")]
        public void HabitacionConDatosInvalidos_NoEsValida(int capacidad, decimal precio, int piso, string errorEsperado)
        {
            var habitacion = new Habitacion { Numero = "101", Capacidad = capacidad, PrecioNoche = precio, IdPiso = piso };
            Assert.Equal([errorEsperado], Errores(habitacion));
        }

        [Fact]
        public void EmpleadoConSalarioNegativo_NoEsValido()
        {
            var empleado = new Empleado
            {
                Nombre = "Eva", Apellido = "Ruiz", Cedula = "2", Telefono = "3", Cargo = "X",
                Correo = "eva@example.com", Salario = -1,
            };
            Assert.Equal(["El salario no puede ser negativo."], Errores(empleado));
        }

        [Fact]
        public void UsuarioSinClave_NoEsValido()
        {
            Assert.Equal(["La clave es obligatoria."], Errores(new Usuario { NombreUsuario = "admin", Rol = "Admin" }));
        }
    }
}
