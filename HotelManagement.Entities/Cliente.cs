using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Correo { get; set; } = string.Empty;
    }
}
