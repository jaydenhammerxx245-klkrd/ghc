using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Empleado
    {
        [Display(Name = "Id")]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es obligatorio.")]
        public string Cargo { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "999999999", ErrorMessage = "El salario no puede ser negativo.")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;
    }
}
