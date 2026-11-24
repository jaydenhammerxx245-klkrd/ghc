namespace HotelManagement.DataSQL
{
    public static class BuildSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Build";

        public const string ObtenerPorId =
            "SELECT * FROM Build WHERE IdBuild=@IdBuild";

        public const string Insertar =
            @"INSERT INTO Build
              (Nombre,Descripcion,CantidadPisos)
              VALUES
              (@Nombre,@Descripcion,@CantidadPisos)";

        public const string Actualizar =
            @"UPDATE Build
              SET Nombre=@Nombre,
                  Descripcion=@Descripcion,
                  CantidadPisos=@CantidadPisos
              WHERE IdBuild=@IdBuild";

        public const string Eliminar =
            "DELETE FROM Build WHERE IdBuild=@IdBuild";
    }
}
