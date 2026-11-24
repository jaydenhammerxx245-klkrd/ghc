namespace HotelManagement.DataSQL
{
    public static class HabitacionSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Habitacion";

        public const string ObtenerPorId =
            "SELECT * FROM Habitacion WHERE IdHabitacion=@IdHabitacion";

        public const string Insertar =
            @"INSERT INTO Habitacion
              (Numero,Tipo,Precio,Disponible,IdPiso)
              VALUES
              (@Numero,@Tipo,@Precio,@Disponible,@IdPiso)";

        public const string Actualizar =
            @"UPDATE Habitacion
              SET Numero=@Numero,
                  Tipo=@Tipo,
                  Precio=@Precio,
                  Disponible=@Disponible,
                  IdPiso=@IdPiso
              WHERE IdHabitacion=@IdHabitacion";

        public const string Eliminar =
            "DELETE FROM Habitacion WHERE IdHabitacion=@IdHabitacion";
    }
}
