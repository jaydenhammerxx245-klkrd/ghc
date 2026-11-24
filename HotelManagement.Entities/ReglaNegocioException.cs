namespace HotelManagement.Entities
{
    /// <summary>
    /// Se lanza cuando una operación viola una regla del negocio
    /// (por ejemplo, una reserva con fechas inválidas o una habitación ocupada).
    /// </summary>
    public class ReglaNegocioException : Exception
    {
        public ReglaNegocioException(string mensaje) : base(mensaje)
        {
        }
    }
}
