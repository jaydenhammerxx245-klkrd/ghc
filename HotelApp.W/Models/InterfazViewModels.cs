using HotelManagement.Entities;

namespace HotelApp.W.Models
{
    /// <summary>Encabezado visual de cada módulo (título, descripción, imagen y botón "Nuevo").</summary>
    public record BannerViewModel(string Titulo, string Descripcion, string? Imagen = null, string? TextoBotonNuevo = null);

    /// <summary>Datos del panel de inicio.</summary>
    public class InicioViewModel
    {
        public int Reservas { get; init; }
        public int Habitaciones { get; init; }
        public int HabitacionesDisponibles { get; init; }
        public int Clientes { get; init; }
        public int Empleados { get; init; }
        public decimal IngresosReservados { get; init; }
        public List<Reserva> ProximasReservas { get; init; } = [];
    }
}
