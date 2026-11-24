namespace HotelManagement.DataSQL
{
    public static class PisoSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Piso";

        public const string ObtenerPorId =
            "SELECT * FROM Piso WHERE IdPiso=@IdPiso";

        public const string Insertar =
            @"INSERT INTO Piso
              (NumeroPiso,Nombre,IdBuild)
              VALUES
              (@NumeroPiso,@Nombre,@IdBuild)";

        public const string Actualizar =
            @"UPDATE Piso
              SET NumeroPiso=@NumeroPiso,
                  Nombre=@Nombre,
                  IdBuild=@IdBuild
              WHERE IdPiso=@IdPiso";

        public const string Eliminar =
            "DELETE FROM Piso WHERE IdPiso=@IdPiso";
    }
}