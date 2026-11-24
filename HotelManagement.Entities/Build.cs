using System;
using System.Collections.Generic;
using System.Text;


    namespace HotelManagement.Entities
    {
        public class Build
        {
            public int IdBuild { get; set; }

            public required string Nombre { get; set; }

            public required string Descripcion { get; set; }

            public int CantidadPisos { get; set; } = 5;
        }
    }

