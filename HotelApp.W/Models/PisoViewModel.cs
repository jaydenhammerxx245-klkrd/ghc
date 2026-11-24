 using System.ComponentModel.DataAnnotations;

    namespace HotelApp.W.Models
    {
        public class PisoViewModel
        {
            public int IdPiso { get; set; }

            [Display(Name = "Número de Piso")]
            public int NumeroPiso { get; set; }

            [Display(Name = "Bloque")]
            public int IdBuild { get; set; }

            public required string Nombre { get; set; }
        }
    }

