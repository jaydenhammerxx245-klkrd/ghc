using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class BuildController : Controller
    {
        private readonly IBuildBL _buildBL;

        public BuildController(IBuildBL buildBL)
        {
            _buildBL = buildBL;
        }

        public async Task<IActionResult> Index()
        {
            var builds = await _buildBL.ObtenerTodosAsync();
            return View(builds);
        }

        public async Task<IActionResult> Details(int id)
        {
            var build = await _buildBL.ObtenerPorIdAsync(id);

            if (build == null)
                return NotFound();

            return View(build);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Build build)
        {
            if (ModelState.IsValid)
            {
                await _buildBL.CrearAsync(build);
                return RedirectToAction(nameof(Index));
            }

            return View(build);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var build = await _buildBL.ObtenerPorIdAsync(id);

            if (build == null)
                return NotFound();

            return View(build);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Build build)
        {
            if (ModelState.IsValid)
            {
                await _buildBL.ActualizarAsync(build);
                return RedirectToAction(nameof(Index));
            }

            return View(build);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var build = await _buildBL.ObtenerPorIdAsync(id);

            if (build == null)
                return NotFound();

            ViewBag.Descripcion = $"el bloque \"{build.Nombre}\"";
            return View("ConfirmDelete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _buildBL.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
