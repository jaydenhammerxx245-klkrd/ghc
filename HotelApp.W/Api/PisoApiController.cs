using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoApiController : ControllerBase
    {
        private readonly IPisoBL _pisoBL;

        public PisoApiController(IPisoBL pisoBL)
        {
            _pisoBL = pisoBL;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Piso>>> Get()
        {
            var pisos = await _pisoBL.ObtenerTodosAsync();
            return Ok(pisos);
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<Piso>> Get(int id)
        {
            var piso = await _pisoBL.ObtenerPorIdAsync(id);

            if (piso == null)
                return NotFound();

            return Ok(piso);
        }

         [HttpPost]
        public async Task<IActionResult> Post(Piso piso)
        {
            await _pisoBL.CrearAsync(piso);

            return Ok(new
            {
                mensaje = "Piso creado correctamente"
            });
        }

         
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Piso piso)
        {
            if (id != piso.IdPiso)
                return BadRequest();

            await _pisoBL.ActualizarAsync(piso);

            return Ok(new
            {
                mensaje = "Piso actualizado correctamente"
            });
        }

         [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pisoBL.EliminarAsync(id);

            return Ok(new
            {
                mensaje = "Piso eliminado correctamente"
            });
        }
    }
}