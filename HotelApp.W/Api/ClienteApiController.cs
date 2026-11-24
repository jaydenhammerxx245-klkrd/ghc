using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteApiController : ControllerBase
    {
        private readonly IClienteBL _clienteBL;

        public ClienteApiController(IClienteBL clienteBL)
        {
            _clienteBL = clienteBL;
        }

         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var clientes = await _clienteBL.GetAllAsync();
            return Ok(clientes);
        }

         
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _clienteBL.GetByIdAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

         
        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            await _clienteBL.AddAsync(cliente);

            return Ok(new
            {
                mensaje = "Cliente creado correctamente"
            });
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
                return BadRequest();

            await _clienteBL.UpdateAsync(cliente);

            return Ok(new
            {
                mensaje = "Cliente actualizado correctamente"
            });
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteBL.DeleteAsync(id);

            return Ok(new
            {
                mensaje = "Cliente eliminado correctamente"
            });
        }
    }
}