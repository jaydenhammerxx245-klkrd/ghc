namespace HotelApp.W.Models
{
    public class EmpleadoViewModel
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