using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>API REST de habitaciones.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class HabitacionApiController : ControllerBase
    {
        private readonly IHabitacionBL _habitacionBL;

        public HabitacionApiController(IHabitacionBL habitacionBL)
        {
            _habitacionBL = habitacionBL;
        }

        /// <summary>Lista todos los registros de habitaciones.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Habitacion>>> Get()
        {
            return Ok(await _habitacionBL.GetAllAsync());
        }

        /// <summary>Obtiene un registro de habitaciones por su id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Habitacion>> GetById(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);
            return habitacion == null ? NotFound() : Ok(habitacion);
        }

        /// <summary>Crea un registro de habitaciones.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Habitacion>> Post(Habitacion habitacion)
        {
            habitacion.IdHabitacion = 0;
            await _habitacionBL.AddAsync(habitacion);
            return CreatedAtAction(nameof(GetById), new { id = habitacion.IdHabitacion }, habitacion);
        }

        /// <summary>Actualiza un registro de habitaciones.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Habitacion>> Put(int id, Habitacion habitacion)
        {
            if (id != habitacion.IdHabitacion)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            if (!await _habitacionBL.UpdateAsync(habitacion))
                return NotFound();

            return Ok(habitacion);
        }

        /// <summary>Elimina un registro (409 si tiene reservas) de habitaciones.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _habitacionBL.DeleteAsync(id) ? NoContent() : NotFound();
            }
            catch (ReglaNegocioException ex)
            {
                // No se puede borrar porque tiene reservas asociadas.
                return Problem(title: "Regla de negocio violada", detail: ex.Message,
                    statusCode: StatusCodes.Status409Conflict);
            }
        }
    }
}
