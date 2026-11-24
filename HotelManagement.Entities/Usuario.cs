using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La clave es obligatoria.")]
        public string Clave { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string Rol { get; set; } = string.Empty;
    }
}
