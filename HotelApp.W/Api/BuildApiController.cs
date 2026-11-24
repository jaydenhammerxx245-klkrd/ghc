using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>API REST de edificios (Build).</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BuildApiController : ControllerBase
    {
        private readonly IBuildBL _buildBL;

        public BuildApiController(IBuildBL buildBL)
        {
            _buildBL = buildBL;
        }

        /// <summary>Lista todos los registros de edificios (Build).</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Build>>> Get()
        {
            return Ok(await _buildBL.ObtenerTodosAsync());
        }

        /// <summary>Obtiene un registro de edificios (Build) por su id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Build>> GetById(int id)
        {
            var build = await _buildBL.ObtenerPorIdAsync(id);
            return build == null ? NotFound() : Ok(build);
        }

        /// <summary>Crea un registro de edificios (Build).</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Build>> Post(Build build)
        {
            build.IdBuild = 0;
            await _buildBL.CrearAsync(build);
            return CreatedAtAction(nameof(GetById), new { id = build.IdBuild }, build);
        }

        /// <summary>Actualiza un registro de edificios (Build).</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Build>> Put(int id, Build build)
        {
            if (id != build.IdBuild)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            if (!await _buildBL.ActualizarAsync(build))
                return NotFound();

            return Ok(build);
        }

        /// <summary>Elimina un registro de edificios (Build).</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _buildBL.EliminarAsync(id) ? NoContent() : NotFound();
        }
    }
}
