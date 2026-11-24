using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApiController : ControllerBase
    {
        private readonly IUsuarioBL _usuarioBL;

        public UsuarioApiController(IUsuarioBL usuarioBL)
        {
            _usuarioBL = usuarioBL;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _usuarioBL.ObtenerTodosAsync();
            return Ok(usuarios);
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await _usuarioBL.ObtenerPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // POST: api/UsuarioApi
        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            await _usuarioBL.CrearAsync(usuario);

            return Ok(new
            {
                mensaje = "Usuario creado correctamente"
            });
        }

         
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest();

            await _usuarioBL.ActualizarAsync(usuario);

            return Ok(new
            {
                mensaje = "Usuario actualizado correctamente"
            });
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _usuarioBL.EliminarAsync(id);

            return Ok(new
            {
                mensaje = "Usuario eliminado correctamente"
            });
        }
    }
}