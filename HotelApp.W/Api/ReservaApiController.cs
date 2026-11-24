using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaApiController : ControllerBase
    {
        private readonly IReservaBL _reservaBL;

        public ReservaApiController(IReservaBL reservaBL)
        {
            _reservaBL = reservaBL;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> Get()
        {
            var reservas = await _reservaBL.ObtenerTodosAsync();
            return Ok(reservas);
        }

         
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> Get(int id)
        {
            var reserva = await _reservaBL.ObtenerPorIdAsync(id);

            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }

         [HttpPost]
        public async Task<IActionResult> Post(Reserva reserva)
        {
            await _reservaBL.CrearAsync(reserva);

            return Ok(new
            {
                mensaje = "Reserva creada correctamente"
            });
        }

         
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Reserva reserva)
        {
            if (id != reserva.IdReserva)
                return BadRequest();

            await _reservaBL.ActualizarAsync(reserva);

            return Ok(new
            {
                mensaje = "Reserva actualizada correctamente"
            });
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reservaBL.EliminarAsync(id);

            return Ok(new
            {
                mensaje = "Reserva eliminada correctamente"
            });
        }
    }
}