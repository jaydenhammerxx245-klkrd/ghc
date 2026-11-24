using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Build
    {
        public int IdBuild { get; set; }

        [Required(ErrorMessage = "El nombre del edificio es obligatorio.")]
        [Display(Name = "Nombre del edificio")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [Display(Name = "Descripción")]
        public required string Descripcion { get; set; }

        [Range(1, 200, ErrorMessage = "La cantidad de pisos debe estar entre 1 y 200.")]
        [Display(Name = "Cantidad de pisos")]
        public int CantidadPisos { get; set; } = 5;
    }
}
