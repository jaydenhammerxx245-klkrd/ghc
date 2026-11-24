using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelApp.W.Controllers
{
    public class PisoController : Controller
    {
        private readonly IPisoBL _pisoBL;
        private readonly IBuildBL _buildBL;

        public PisoController(IPisoBL pisoBL, IBuildBL buildBL)
        {
            _pisoBL = pisoBL;
            _buildBL = buildBL;
        }

        // Llena el desplegable de bloques que usan las vistas Create/Edit.
        private async Task CargarBuildsAsync(int? seleccionado = null)
        {
            var builds = await _buildBL.ObtenerTodosAsync();
            ViewBag.Builds = new SelectList(builds, nameof(Build.IdBuild), nameof(Build.Nombre), seleccionado);
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

        public async Task<IActionResult> Create()
        {
            await CargarBuildsAsync();
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

            await CargarBuildsAsync(piso.IdBuild);
            return View(piso);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var piso = await _pisoBL.ObtenerPorIdAsync(id);

            if (piso == null)
                return NotFound();

            await CargarBuildsAsync(piso.IdBuild);
            return View(piso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Piso piso)
        {
            if (ModelState.IsValid)
            {
                await _pisoBL.ActualizarAsync(piso);
                return RedirectToAction(nameof(Index));
            }

            await CargarBuildsAsync(piso.IdBuild);
            return View(piso);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var piso = await _pisoBL.ObtenerPorIdAsync(id);

            if (piso == null)
                return NotFound();

            ViewBag.Descripcion = $"el piso \"{piso.Nombre}\"";
            return View("ConfirmDelete", id);
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
