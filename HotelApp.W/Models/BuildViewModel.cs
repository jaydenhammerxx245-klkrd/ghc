
    using System.ComponentModel.DataAnnotations;

    namespace HotelApp.W.Models
    {
        public class BuildViewModel
        {
            public int IdBuild { get; set; }

            [Required]
            [Display(Name = "Nombre del Bloque")]
            public required string Nombre { get; set; }

            public required string Descripcion { get; set; }

            [Display(Name = "Cantidad de Pisos")]
            public int CantidadPisos { get; set; }
        }
    }

