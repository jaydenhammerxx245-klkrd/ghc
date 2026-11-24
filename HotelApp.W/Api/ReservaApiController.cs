using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>API REST de reservas. Aplica las reglas de negocio de ReservaBL.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ReservaApiController : ControllerBase
    {
        private readonly IReservaBL _reservaBL;

        public ReservaApiController(IReservaBL reservaBL)
        {
            _reservaBL = reservaBL;
        }

        /// <summary>Lista todas las reservas.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        {
            return Ok(await _reservaBL.ObtenerTodosAsync());
        }

        /// <summary>Obtiene una reserva por su id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Reserva>> GetById(int id)
        {
            var reserva = await _reservaBL.ObtenerPorIdAsync(id);
            return reserva == null ? NotFound() : Ok(reserva);
        }

        /// <summary>
        /// Crea una reserva. El total se calcula automáticamente (noches × precio por noche).
        /// Responde 422 si se viola una regla de negocio.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<Reserva>> Post(Reserva reserva)
        {
            try
            {
                await _reservaBL.CrearAsync(reserva);
                return CreatedAtAction(nameof(GetById), new { id = reserva.IdReserva }, reserva);
            }
            catch (ReglaNegocioException ex)
            {
                return ReglaViolada(ex);
            }
        }

        /// <summary>Actualiza una reserva. Responde 422 si se viola una regla de negocio.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<Reserva>> Put(int id, Reserva reserva)
        {
            if (id != reserva.IdReserva)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            try
            {
                if (!await _reservaBL.ActualizarAsync(reserva))
                    return NotFound();

                return Ok(reserva);
            }
            catch (ReglaNegocioException ex)
            {
                return ReglaViolada(ex);
            }
        }

        /// <summary>Elimina una reserva.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _reservaBL.EliminarAsync(id) ? NoContent() : NotFound();
        }

        private ObjectResult ReglaViolada(ReglaNegocioException ex)
        {
            return Problem(
                title: "Regla de negocio violada",
                detail: ex.Message,
                statusCode: StatusCodes.Status422UnprocessableEntity);
        }
    }
}
