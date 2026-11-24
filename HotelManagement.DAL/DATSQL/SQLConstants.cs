namespace HotelManagement.DAL.DATSQL
{
    public static class SQLConstants
    {
        #region CLIENTE

        public const string CLIENTE_GETALL =
            "SELECT * FROM Cliente";

        public const string CLIENTE_GETBYID =
            "SELECT * FROM Cliente WHERE IdCliente = @IdCliente";

        public const string CLIENTE_INSERT =
            @"INSERT INTO Cliente
            (Nombre,Apellido,Telefono,Email)
            VALUES
            (@Nombre,@Apellido,@Telefono,@Email)";

        public const string CLIENTE_UPDATE =
            @"UPDATE Cliente
              SET Nombre=@Nombre,
                  Apellido=@Apellido,
                  Telefono=@Telefono,
                  Email=@Email
              WHERE IdCliente=@IdCliente";

        public const string CLIENTE_DELETE =
            "DELETE FROM Cliente WHERE IdCliente=@IdCliente";

        #endregion

        #region BUILD

        public const string BUILD_GETALL =
            "SELECT * FROM Build";

        public const string BUILD_GETBYID =
            "SELECT * FROM Build WHERE IdBuild=@IdBuild";

        public const string BUILD_INSERT =
            @"INSERT INTO Build
            (Nombre,Descripcion,CantidadPisos)
            VALUES
            (@Nombre,@Descripcion,@CantidadPisos)";

        public const string BUILD_UPDATE =
            @"UPDATE Build
              SET Nombre=@Nombre,
                  Descripcion=@Descripcion,
                  CantidadPisos=@CantidadPisos
              WHERE IdBuild=@IdBuild";

        public const string BUILD_DELETE =
            "DELETE FROM Build WHERE IdBuild=@IdBuild";

        #endregion

        #region PISO

        public const string PISO_GETALL =
            "SELECT * FROM Piso";

        public const string PISO_GETBYID =
            "SELECT * FROM Piso WHERE IdPiso=@IdPiso";

        public const string PISO_INSERT =
            @"INSERT INTO Piso
            (NumeroPiso,Nombre,IdBuild)
            VALUES
            (@NumeroPiso,@Nombre,@IdBuild)";

        public const string PISO_UPDATE =
            @"UPDATE Piso
              SET NumeroPiso=@NumeroPiso,
                  Nombre=@Nombre,
                  IdBuild=@IdBuild
              WHERE IdPiso=@IdPiso";

        public const string PISO_DELETE =
            "DELETE FROM Piso WHERE IdPiso=@IdPiso";

        #endregion

        #region HABITACION

        public const string HABITACION_GETALL =
            "SELECT * FROM Habitacion";

        public const string HABITACION_GETBYID =
            "SELECT * FROM Habitacion WHERE IdHabitacion=@IdHabitacion";

        public const string HABITACION_INSERT =
            @"INSERT INTO Habitacion
            (Numero,Tipo,Precio,Disponible,IdPiso)
            VALUES
            (@Numero,@Tipo,@Precio,@Disponible,@IdPiso)";

        public const string HABITACION_UPDATE =
            @"UPDATE Habitacion
              SET Numero=@Numero,
                  Tipo=@Tipo,
                  Precio=@Precio,
                  Disponible=@Disponible,
                  IdPiso=@IdPiso
              WHERE IdHabitacion=@IdHabitacion";

        public const string HABITACION_DELETE =
            "DELETE FROM Habitacion WHERE IdHabitacion=@IdHabitacion";

        #endregion
    }
}
