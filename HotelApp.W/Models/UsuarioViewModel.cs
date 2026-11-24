namespace HotelApp.W.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string Clave { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}
