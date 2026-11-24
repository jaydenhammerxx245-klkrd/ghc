using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteBL _clienteBL;

        public ClienteController(IClienteBL clienteBL)
        {
            _clienteBL = clienteBL;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clienteBL.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteBL.GetByIdAsync(id);
            return cliente == null ? NotFound() : View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            await _clienteBL.AddAsync(cliente);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteBL.GetByIdAsync(id);
            return cliente == null ? NotFound() : View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            if (!await _clienteBL.UpdateAsync(cliente))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteBL.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            ViewBag.Descripcion = $"al cliente {cliente.Nombre} {cliente.Apellido}";
            return View("ConfirmDelete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clienteBL.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
