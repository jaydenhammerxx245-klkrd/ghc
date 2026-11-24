using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;


namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildApiController : ControllerBase
    {
        private readonly IBuildBL _buildBL;

        public BuildApiController(IBuildBL buildBL)
        {
            _buildBL = buildBL;
        }

         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Build>>> Get()
        {
            var builds = await _buildBL.ObtenerTodosAsync();
            return Ok(builds);
        }

         
        [HttpGet("{id}")]
        public async Task<ActionResult<Build>> Get(int id)
        {
            var build = await _buildBL.ObtenerPorIdAsync(id);

            if (build == null)
                return NotFound();

            return Ok(build);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(Build build)
        {
            await _buildBL.CrearAsync(build);

            return Ok(new
            {
                mensaje = "Build creado correctamente"
            });
        }

         
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Build build)
        {
            if (id != build.IdBuild)
                return BadRequest();

            await _buildBL.ActualizarAsync(build);

            return Ok(new
            {
                mensaje = "Build actualizado correctamente"
            });
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _buildBL.EliminarAsync(id);

            return Ok(new
            {
                mensaje = "Build eliminado correctamente"
            });
        }
    }
}