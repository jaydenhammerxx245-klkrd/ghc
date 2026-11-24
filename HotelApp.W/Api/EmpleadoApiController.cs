using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoApiController : ControllerBase
    {
        private readonly IEmpleadoBL _empleadoBL;

        public EmpleadoApiController(IEmpleadoBL empleadoBL)
        {
            _empleadoBL = empleadoBL;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            var empleados = await _empleadoBL.GetAllAsync();
            return Ok(empleados);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await _empleadoBL.GetByIdAsync(id);

            if (empleado == null)
                return NotFound();

            return Ok(empleado);
        }

         
        [HttpPost]
        public async Task<IActionResult> Post(Empleado empleado)
        {
            await _empleadoBL.AddAsync(empleado);

            return Ok(new
            {
                mensaje = "Empleado creado correctamente"
            });
        }

         
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
                return BadRequest();

            await _empleadoBL.UpdateAsync(empleado);

            return Ok(new
            {
                mensaje = "Empleado actualizado correctamente"
            });
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _empleadoBL.DeleteAsync(id);

            return Ok(new
            {
                mensaje = "Empleado eliminado correctamente"
            });
        }
    }
}