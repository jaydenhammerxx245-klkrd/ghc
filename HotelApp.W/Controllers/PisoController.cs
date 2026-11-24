using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class PisoController : Controller
    {
        private readonly IPisoBL _pisoBL;

        public PisoController(IPisoBL pisoBL)
        {
            _pisoBL = pisoBL;
        }

        public async Task<IActionResult> Index()
        {
            var pisos = await _pisoBL.ObtenerTodosAsync();
            return View(pisos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var piso = await _pisoBL.ObtenerPorIdAsync(id);

            if (piso == null)
                return NotFound();

            return View(piso);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         
        public async Task<IActionResult> Create(Piso piso)
        {
            if (ModelState.IsValid)
            {
                await _pisoBL.CrearAsync(piso);
                return RedirectToAction(nameof(Index));
            }

            return View(piso);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Piso piso)
        {
            if (ModelState.IsValid)
            {
                await _pisoBL.ActualizarAsync(piso);
                return RedirectToAction(nameof(Index));
            }

            return View(piso);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var piso = await _pisoBL.ObtenerPorIdAsync(id);

            if (piso == null)
                return NotFound();

            return View(piso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pisoBL.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
