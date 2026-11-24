using HotelApp.W.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class ClienteController : Controller
    {
        private static readonly List<ClienteViewModel> clientes = [];

        public IActionResult Index()
        {
            return View(clientes);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ClienteViewModel cliente)
        {
            cliente.IdCliente = clientes.Count == 0 ? 1 : clientes.Max(c => c.IdCliente) + 1;
            clientes.Add(cliente);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        public IActionResult Edit(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.IdCliente == id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(ClienteViewModel cliente)
        {
            var clienteActual = clientes.FirstOrDefault(c => c.IdCliente == cliente.IdCliente);

            if (clienteActual != null)
            {
                clienteActual.Nombre = cliente.Nombre;
                clienteActual.Apellido = cliente.Apellido;
                clienteActual.Cedula = cliente.Cedula;
                clienteActual.Telefono = cliente.Telefono;
                clienteActual.Correo = cliente.Correo;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.IdCliente == id);

            if (cliente != null)
                clientes.Remove(cliente);

            return RedirectToAction(nameof(Index));
        }
    }
}

