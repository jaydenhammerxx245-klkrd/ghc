namespace HotelManagement.DataSQL
{
    public static class ReservaSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Reserva";

        public const string ObtenerPorId =
            "SELECT * FROM Reserva WHERE IdReserva=@IdReserva";

        public const string Insertar =
            @"INSERT INTO Reserva
            (IdCliente,IdHabitacion,FechaEntrada,FechaSalida)
            VALUES
            (@IdCliente,@IdHabitacion,@FechaEntrada,@FechaSalida)";

        public const string Actualizar =
            @"UPDATE Reserva
              SET IdCliente=@IdCliente,
                  IdHabitacion=@IdHabitacion,
                  FechaEntrada=@FechaEntrada,
                  FechaSalida=@FechaSalida
              WHERE IdReserva=@IdReserva";

        public const string Eliminar =
            "DELETE FROM Reserva WHERE IdReserva=@IdReserva";
    }
}