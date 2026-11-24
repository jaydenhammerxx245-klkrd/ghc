using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Habitacion
    {
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El número de habitación es obligatorio.")]
        [Display(Name = "Número")]
        public string Numero { get; set; } = string.Empty;

        [Range(1, 50, ErrorMessage = "La capacidad debe estar entre 1 y 50 personas.")]
        public int Capacidad { get; set; }

        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El precio por noche debe ser mayor que cero.")]
        [Display(Name = "Precio por noche")]
        public decimal PrecioNoche { get; set; }

        [Display(Name = "Disponible para reservar")]
        public bool Disponible { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar el piso.")]
        [Display(Name = "Piso")]
        public int IdPiso { get; set; }

        [Range(typeof(decimal), "0", "999999999", ErrorMessage = "El precio no puede ser negativo.")]
        [Display(Name = "Precio de lista")]
        public decimal Precio { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;
    }
}
