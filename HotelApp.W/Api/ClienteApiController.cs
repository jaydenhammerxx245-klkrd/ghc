using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>API REST de clientes.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClienteApiController : ControllerBase
    {
        private readonly IClienteBL _clienteBL;

        public ClienteApiController(IClienteBL clienteBL)
        {
            _clienteBL = clienteBL;
        }

        /// <summary>Lista todos los registros de clientes.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            return Ok(await _clienteBL.GetAllAsync());
        }

        /// <summary>Obtiene un registro de clientes por su id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await _clienteBL.GetByIdAsync(id);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        /// <summary>Crea un registro de clientes.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            cliente.IdCliente = 0;
            await _clienteBL.AddAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);
        }

        /// <summary>Actualiza un registro de clientes.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cliente>> Put(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            if (!await _clienteBL.UpdateAsync(cliente))
                return NotFound();

            return Ok(cliente);
        }

        /// <summary>Elimina un registro (409 si tiene reservas) de clientes.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _clienteBL.DeleteAsync(id) ? NoContent() : NotFound();
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
