using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>API REST de pisos.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PisoApiController : ControllerBase
    {
        private readonly IPisoBL _pisoBL;

        public PisoApiController(IPisoBL pisoBL)
        {
            _pisoBL = pisoBL;
        }

        /// <summary>Lista todos los registros de pisos.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Piso>>> Get()
        {
            return Ok(await _pisoBL.ObtenerTodosAsync());
        }

        /// <summary>Obtiene un registro de pisos por su id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Piso>> GetById(int id)
        {
            var piso = await _pisoBL.ObtenerPorIdAsync(id);
            return piso == null ? NotFound() : Ok(piso);
        }

        /// <summary>Crea un registro de pisos.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Piso>> Post(Piso piso)
        {
            piso.IdPiso = 0;
            await _pisoBL.CrearAsync(piso);
            return CreatedAtAction(nameof(GetById), new { id = piso.IdPiso }, piso);
        }

        /// <summary>Actualiza un registro de pisos.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Piso>> Put(int id, Piso piso)
        {
            if (id != piso.IdPiso)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            if (!await _pisoBL.ActualizarAsync(piso))
                return NotFound();

            return Ok(piso);
        }

        /// <summary>Elimina un registro de pisos.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _pisoBL.EliminarAsync(id) ? NoContent() : NotFound();
        }
    }
}
