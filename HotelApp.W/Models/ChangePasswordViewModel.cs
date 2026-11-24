using System.ComponentModel.DataAnnotations;

namespace HotelApp.W.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string ClaveActual { get; set; } = string.Empty;

        [Required]
        public string NuevaClave { get; set; } = string.Empty;

        [Required]
        public string ConfirmarClave { get; set; } = string.Empty;
    }
}