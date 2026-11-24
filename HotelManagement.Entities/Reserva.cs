namespace HotelManagement.Entities
{
    public class Reserva
    {
        public int IdReserva { get; set; }

        public int IdCliente { get; set; }

        public int IdHabitacion { get; set; }

        public DateTime FechaEntrada { get; set; }

        public DateTime FechaSalida { get; set; }

        public decimal Total { get; set; }
    }
}