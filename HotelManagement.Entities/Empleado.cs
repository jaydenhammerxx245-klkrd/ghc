using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagement.Entities
{
     public class Empleado
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Cedula { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Cargo { get; set; } = string.Empty;

        public decimal Salario { get; set; }
        public string Correo { get; set; } = string.Empty;
    }
}
