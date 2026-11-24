using System;
using System.Collections.Generic;
using System.Text;


    namespace HotelManagement.Entities
    {
        public class Piso
        {
            public int IdPiso { get; set; }

            public int NumeroPiso { get; set; }

            public int IdBuild { get; set; }

            public required string Nombre { get; set; }
        }
    }

