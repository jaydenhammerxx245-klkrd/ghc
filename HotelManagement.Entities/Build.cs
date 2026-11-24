using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Build
    {
        public int IdBuild { get; set; }

        [Required(ErrorMessage = "El nombre del edificio es obligatorio.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public required string Descripcion { get; set; }

        [Range(1, 200, ErrorMessage = "La cantidad de pisos debe estar entre 1 y 200.")]
        public int CantidadPisos { get; set; } = 5;
    }
}
