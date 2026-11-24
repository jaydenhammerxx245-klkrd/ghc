namespace HotelManagement.DataSQL
{
    public static class EmpleadoSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Empleado";

        public const string ObtenerPorId =
            "SELECT * FROM Empleado WHERE IdEmpleado=@IdEmpleado";

        public const string Insertar =
            @"INSERT INTO Empleado
            (Nombre,Cargo,Telefono)
            VALUES
            (@Nombre,@Cargo,@Telefono)";

        public const string Actualizar =
            @"UPDATE Empleado
              SET Nombre=@Nombre,
                  Cargo=@Cargo,
                  Telefono=@Telefono
              WHERE IdEmpleado=@IdEmpleado";

        public const string Eliminar =
            "DELETE FROM Empleado WHERE IdEmpleado=@IdEmpleado";
    }
}