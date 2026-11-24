using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Piso
    {
        public int IdPiso { get; set; }

        [Range(0, 200, ErrorMessage = "El número de piso debe estar entre 0 y 200.")]
        [Display(Name = "Número de piso")]
        public int NumeroPiso { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar el edificio.")]
        [Display(Name = "Edificio")]
        public int IdBuild { get; set; }

        [Required(ErrorMessage = "El nombre del piso es obligatorio.")]
        [Display(Name = "Nombre del piso")]
        public required string Nombre { get; set; }
    }
}
