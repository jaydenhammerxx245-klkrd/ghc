using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>API REST de empleados.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmpleadoApiController : ControllerBase
    {
        private readonly IEmpleadoBL _empleadoBL;

        public EmpleadoApiController(IEmpleadoBL empleadoBL)
        {
            _empleadoBL = empleadoBL;
        }

        /// <summary>Lista todos los registros de empleados.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            return Ok(await _empleadoBL.GetAllAsync());
        }

        /// <summary>Obtiene un registro de empleados por su id.</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Empleado>> GetById(int id)
        {
            var empleado = await _empleadoBL.GetByIdAsync(id);
            return empleado == null ? NotFound() : Ok(empleado);
        }

        /// <summary>Crea un registro de empleados.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Empleado>> Post(Empleado empleado)
        {
            empleado.IdEmpleado = 0;
            await _empleadoBL.AddAsync(empleado);
            return CreatedAtAction(nameof(GetById), new { id = empleado.IdEmpleado }, empleado);
        }

        /// <summary>Actualiza un registro de empleados.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Empleado>> Put(int id, Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            if (!await _empleadoBL.UpdateAsync(empleado))
                return NotFound();

            return Ok(empleado);
        }

        /// <summary>Elimina un registro de empleados.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _empleadoBL.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
