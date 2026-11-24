using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    /// <summary>Datos de un usuario que devuelve la API (nunca incluye la clave).</summary>
    public record UsuarioRespuesta(int IdUsuario, string NombreUsuario, string Rol)
    {
        public static UsuarioRespuesta Desde(Usuario u) => new(u.IdUsuario, u.NombreUsuario, u.Rol);
    }

    /// <summary>API REST de usuarios.</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioApiController : ControllerBase
    {
        private readonly IUsuarioBL _usuarioBL;

        public UsuarioApiController(IUsuarioBL usuarioBL)
        {
            _usuarioBL = usuarioBL;
        }

        /// <summary>Lista todos los usuarios (sin la clave).</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UsuarioRespuesta>>> Get()
        {
            var usuarios = await _usuarioBL.ObtenerTodosAsync();
            return Ok(usuarios.Select(UsuarioRespuesta.Desde));
        }

        /// <summary>Obtiene un usuario por su id (sin la clave).</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioRespuesta>> GetById(int id)
        {
            var usuario = await _usuarioBL.ObtenerPorIdAsync(id);
            return usuario == null ? NotFound() : Ok(UsuarioRespuesta.Desde(usuario));
        }

        /// <summary>Crea un usuario.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UsuarioRespuesta>> Post(Usuario usuario)
        {
            usuario.IdUsuario = 0;
            await _usuarioBL.CrearAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, UsuarioRespuesta.Desde(usuario));
        }

        /// <summary>Actualiza un usuario.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioRespuesta>> Put(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest(new { mensaje = "El id de la URL no coincide con el del cuerpo." });

            if (!await _usuarioBL.ActualizarAsync(usuario))
                return NotFound();

            return Ok(UsuarioRespuesta.Desde(usuario));
        }

        /// <summary>Elimina un usuario.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _usuarioBL.EliminarAsync(id) ? NoContent() : NotFound();
        }
    }
}
