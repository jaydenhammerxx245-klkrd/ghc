

namespace HotelManagement.Entities
{
    public class Habitacion
    {
        public int IdHabitacion { get; set; }

        public string Numero { get; set; } = string.Empty;

        public int Capacidad { get; set; }

        public decimal PrecioNoche { get; set; }

        public bool Disponible { get; set; }

        public int IdPiso { get; set; }

        public decimal Precio { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty; 
    }
}