namespace HotelManagement.DataSQL
{
    public static class ClienteSQL
    {
        public const string ObtenerTodos =
            "SELECT * FROM Cliente";

        public const string ObtenerPorId =
            "SELECT * FROM Cliente WHERE IdCliente=@IdCliente";

        public const string Insertar =
            @"INSERT INTO Cliente
            (Nombre,Apellido,Telefono,Email)
            VALUES
            (@Nombre,@Apellido,@Telefono,@Email)";

        public const string Actualizar =
            @"UPDATE Cliente
              SET Nombre=@Nombre,
                  Apellido=@Apellido,
                  Telefono=@Telefono,
                  Email=@Email
              WHERE IdCliente=@IdCliente";

        public const string Eliminar =
            "DELETE FROM Cliente WHERE IdCliente=@IdCliente";
    }
}