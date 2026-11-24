using System.ComponentModel.DataAnnotations;

namespace HotelApp.W.Models
{
   
    public class HabitacionViewModel
    {
        public int IdHabitacion { get; set; }

        public string Numero { get; set; } = string.Empty;

        public int Capacidad { get; set; }

        public decimal PrecioNoche { get; set; }

        public bool Disponible { get; set; }

        [Display(Name = "Piso")]
        public int IdPiso { get; set; }
    }
}

