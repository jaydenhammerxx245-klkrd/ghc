using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionApiController : ControllerBase
    {
        private readonly IHabitacionBL _habitacionBL;

        public HabitacionApiController(IHabitacionBL habitacionBL)
        {
            _habitacionBL = habitacionBL;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitacion>>> Get()
        {
            var habitaciones = await _habitacionBL.GetAllAsync();
            return Ok(habitaciones);
        }

         
        [HttpGet("{id}")]
        public async Task<ActionResult<Habitacion>> Get(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);

            if (habitacion == null)
                return NotFound();

            return Ok(habitacion);
        }

         
        [HttpPost]
        public async Task<IActionResult> Post(Habitacion habitacion)
        {
            await _habitacionBL.AddAsync(habitacion);

            return Ok(new
            {
                mensaje = "Habitación creada correctamente"
            });
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Habitacion habitacion)
        {
            if (id != habitacion.IdHabitacion)
                return BadRequest();

            await _habitacionBL.UpdateAsync(habitacion);

            return Ok(new
            {
                mensaje = "Habitación actualizada correctamente"
            });
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _habitacionBL.DeleteAsync(id);

            return Ok(new
            {
                mensaje = "Habitación eliminada correctamente"
            });
        }
    }
}