using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HotelManagement.IntegrationTests
{
    /// <summary>
    /// Levanta la aplicación completa en memoria con una base SQLite temporal (el mismo esquema)
    /// para cada prueba, y verifica los códigos HTTP de la capa de API.
    /// </summary>
    public sealed class ApiHttpTests : IDisposable
    {
        private readonly string _archivoBd = Path.Combine(Path.GetTempPath(), $"hotelcrown-test-{Guid.NewGuid():N}.db");
        private readonly WebApplicationFactory<Program> _fabrica;
        private readonly HttpClient _http;

        public ApiHttpTests()
        {
            _fabrica = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
            {
                b.UseEnvironment("Development");
                b.UseSetting("DatabaseProvider", "Sqlite");
                b.UseSetting("ConnectionStrings:SqliteConnection", $"Data Source={_archivoBd};Pooling=False");
            });
            _http = _fabrica.CreateClient();
        }

        private async Task<HttpResponseMessage> Post(string ruta, object cuerpo) => await _http.PostAsJsonAsync(ruta, cuerpo);

        private static async Task<JsonElement> Json(HttpResponseMessage r) =>
            JsonDocument.Parse(await r.Content.ReadAsStringAsync()).RootElement;

        /// <summary>Crea un cliente (id 1) y las habitaciones 101 (disponible, id 1) y 102 (no disponible, id 2).</summary>
        private async Task CrearDatosBaseAsync()
        {
            Assert.Equal(HttpStatusCode.Created, (await Post("/api/ClienteApi", new
            {
                nombre = "Luis", apellido = "Paz", cedula = "1", telefono = "2", correo = "luis@example.com",
            })).StatusCode);
            Assert.Equal(HttpStatusCode.Created, (await Post("/api/HabitacionApi", new
            {
                numero = "101", capacidad = 2, precioNoche = 80, disponible = true, idPiso = 1,
            })).StatusCode);
            Assert.Equal(HttpStatusCode.Created, (await Post("/api/HabitacionApi", new
            {
                numero = "102", capacidad = 2, precioNoche = 120, disponible = false, idPiso = 1,
            })).StatusCode);
        }

        [Fact]
        public async Task ReservaValida_Responde201_ConTotalCalculado()
        {
            await CrearDatosBaseAsync();

            var r = await Post("/api/ReservaApi", new { idCliente = 1, idHabitacion = 1, fechaEntrada = "2026-12-01", fechaSalida = "2026-12-04" });

            Assert.Equal(HttpStatusCode.Created, r.StatusCode);
            Assert.EndsWith("/api/ReservaApi/1", r.Headers.Location!.ToString());
            Assert.Equal(240m, (await Json(r)).GetProperty("total").GetDecimal());
        }

        [Theory]
        [InlineData(1, "2026-12-03", "2026-12-06", "ya está reservada")]
        [InlineData(1, "2026-12-10", "2026-12-08", "fecha de salida")]
        [InlineData(2, "2026-12-10", "2026-12-12", "no está disponible")]
        public async Task ReservaQueViolaUnaRegla_Responde422_ConElMotivo(int habitacion, string entrada, string salida, string motivo)
        {
            await CrearDatosBaseAsync();
            await Post("/api/ReservaApi", new { idCliente = 1, idHabitacion = 1, fechaEntrada = "2026-12-01", fechaSalida = "2026-12-04" });

            var r = await Post("/api/ReservaApi", new { idCliente = 1, idHabitacion = habitacion, fechaEntrada = entrada, fechaSalida = salida });

            Assert.Equal(HttpStatusCode.UnprocessableEntity, r.StatusCode);
            Assert.Contains(motivo, (await Json(r)).GetProperty("detail").GetString());
        }

        [Fact]
        public async Task DatosObligatoriosFaltantes_Responde400()
        {
            var r = await Post("/api/ClienteApi", new { });

            Assert.Equal(HttpStatusCode.BadRequest, r.StatusCode);
            Assert.Contains("El nombre es obligatorio.", await r.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task HabitacionConPrecioNegativo_Responde400()
        {
            var r = await Post("/api/HabitacionApi", new { numero = "103", capacidad = 2, precioNoche = -80, idPiso = 1 });

            Assert.Equal(HttpStatusCode.BadRequest, r.StatusCode);
            Assert.Contains("mayor que cero", await r.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task IdInexistente_Responde404_EnGetPutYDelete()
        {
            Assert.Equal(HttpStatusCode.NotFound, (await _http.GetAsync("/api/BuildApi/99")).StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, (await _http.PutAsJsonAsync("/api/BuildApi/99",
                new { idBuild = 99, nombre = "X", descripcion = "X", cantidadPisos = 1 })).StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, (await _http.DeleteAsync("/api/BuildApi/99")).StatusCode);
        }

        [Fact]
        public async Task IdDeLaUrlDistintoAlDelCuerpo_Responde400()
        {
            var r = await _http.PutAsJsonAsync("/api/BuildApi/1", new { idBuild = 2, nombre = "X", descripcion = "X", cantidadPisos = 1 });
            Assert.Equal(HttpStatusCode.BadRequest, r.StatusCode);
        }

        [Fact]
        public async Task BorrarClienteConReservas_Responde409_YNoLoBorra()
        {
            await CrearDatosBaseAsync();
            await Post("/api/ReservaApi", new { idCliente = 1, idHabitacion = 1, fechaEntrada = "2026-12-01", fechaSalida = "2026-12-04" });

            var r = await _http.DeleteAsync("/api/ClienteApi/1");

            Assert.Equal(HttpStatusCode.Conflict, r.StatusCode);
            Assert.Equal(HttpStatusCode.OK, (await _http.GetAsync("/api/ClienteApi/1")).StatusCode);
        }

        [Fact]
        public async Task CrudCompleto_PorHttp()
        {
            var crear = await Post("/api/BuildApi", new { nombre = "Torre A", descripcion = "Principal", cantidadPisos = 5 });
            Assert.Equal(HttpStatusCode.Created, crear.StatusCode);

            var editar = await _http.PutAsJsonAsync("/api/BuildApi/1", new { idBuild = 1, nombre = "Torre A", descripcion = "Remodelado", cantidadPisos = 6 });
            Assert.Equal(HttpStatusCode.OK, editar.StatusCode);
            Assert.Equal("Remodelado", (await Json(await _http.GetAsync("/api/BuildApi/1"))).GetProperty("descripcion").GetString());

            Assert.Equal(HttpStatusCode.NoContent, (await _http.DeleteAsync("/api/BuildApi/1")).StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, (await _http.GetAsync("/api/BuildApi/1")).StatusCode);
        }

        [Fact]
        public async Task UsuarioApi_NuncaDevuelveLaClave()
        {
            await Post("/api/UsuarioApi", new { nombreUsuario = "admin", clave = "S3creta!", rol = "Administrador" });

            var lista = await _http.GetStringAsync("/api/UsuarioApi");
            var uno = await _http.GetStringAsync("/api/UsuarioApi/1");

            Assert.Contains("admin", lista);
            Assert.DoesNotContain("S3creta!", lista + uno);
            Assert.DoesNotContain("clave", (lista + uno).ToLowerInvariant());
        }

        public void Dispose()
        {
            _http.Dispose();
            _fabrica.Dispose();
            if (File.Exists(_archivoBd))
                File.Delete(_archivoBd);
        }
    }
}
