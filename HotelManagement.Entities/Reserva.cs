using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class Reserva
    {
        [Display(Name = "Reserva")]
        public int IdReserva { get; set; }

        [Display(Name = "Cliente")]

        public int IdCliente { get; set; }

        [Display(Name = "Habitación")]

        public int IdHabitacion { get; set; }

        [Display(Name = "Fecha de entrada")]

        public DateTime FechaEntrada { get; set; }

        [Display(Name = "Fecha de salida")]

        public DateTime FechaSalida { get; set; }

        [Display(Name = "Total")]

        public decimal Total { get; set; }

        // ----- Reglas de negocio propias de la reserva -----
        // No dependen de ninguna base de datos: se pueden probar con objetos en memoria.

        /// <summary>Cantidad de noches entre la fecha de entrada y la de salida.</summary>
        public int Noches => (FechaSalida.Date - FechaEntrada.Date).Days;

        /// <summary>Indica si esta reserva se cruza en fechas con otra de la misma habitación.</summary>
        public bool SeSolapaCon(Reserva otra)
        {
            return otra.IdHabitacion == IdHabitacion
                && otra.IdReserva != IdReserva
                && FechaEntrada.Date < otra.FechaSalida.Date
                && otra.FechaEntrada.Date < FechaSalida.Date;
        }

        /// <summary>Valida los datos propios de la reserva.</summary>
        public void ValidarDatos()
        {
            if (IdCliente <= 0)
                throw new ReglaNegocioException("La reserva debe indicar un cliente válido.");

            if (IdHabitacion <= 0)
                throw new ReglaNegocioException("La reserva debe indicar una habitación válida.");

            if (FechaEntrada == default || FechaSalida == default)
                throw new ReglaNegocioException("Debe indicar la fecha de entrada y la fecha de salida.");

            if (FechaSalida.Date <= FechaEntrada.Date)
                throw new ReglaNegocioException("La fecha de salida debe ser posterior a la fecha de entrada.");
        }

        /// <summary>Calcula el total de la estadía según el precio por noche de la habitación.</summary>
        public void CalcularTotal(Habitacion habitacion)
        {
            if (habitacion.PrecioNoche <= 0)
                throw new ReglaNegocioException(
                    $"La habitación {habitacion.Numero} no tiene un precio por noche válido.");

            Total = Noches * habitacion.PrecioNoche;
        }
    }
}
