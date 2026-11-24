namespace HotelManagement.DataSQL
{
    public static class UsuarioSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Usuario";

        public const string ObtenerPorId =
            "SELECT * FROM Usuario WHERE IdUsuario=@IdUsuario";

        public const string Insertar =
            @"INSERT INTO Usuario
            (Username,PasswordHash,Rol)
            VALUES
            (@Username,@PasswordHash,@Rol)";

        public const string Actualizar =
            @"UPDATE Usuario
              SET Username=@Username,
                  PasswordHash=@PasswordHash,
                  Rol=@Rol
              WHERE IdUsuario=@IdUsuario";

        public const string Eliminar =
            "DELETE FROM Usuario WHERE IdUsuario=@IdUsuario";
    }
}