using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioBL _usuarioBL;

        public UsuarioController(IUsuarioBL usuarioBL)
        {
            _usuarioBL = usuarioBL;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioBL.ObtenerTodosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _usuarioBL.ObtenerPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioBL.CrearAsync(usuario);
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioBL.ObtenerPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _usuarioBL.ActualizarAsync(usuario);
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioBL.ObtenerPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            ViewBag.Descripcion = $"el usuario \"{usuario.NombreUsuario}\"";
            return View("ConfirmDelete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuarioBL.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}