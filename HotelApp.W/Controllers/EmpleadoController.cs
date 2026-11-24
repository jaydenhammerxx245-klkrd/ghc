using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoBL _empleadoBL;

        public EmpleadoController(IEmpleadoBL empleadoBL)
        {
            _empleadoBL = empleadoBL;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _empleadoBL.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var empleado = await _empleadoBL.GetByIdAsync(id);
            return empleado == null ? NotFound() : View(empleado);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (!ModelState.IsValid)
                return View(empleado);

            await _empleadoBL.AddAsync(empleado);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var empleado = await _empleadoBL.GetByIdAsync(id);
            return empleado == null ? NotFound() : View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Empleado empleado)
        {
            if (!ModelState.IsValid)
                return View(empleado);

            if (!await _empleadoBL.UpdateAsync(empleado))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _empleadoBL.GetByIdAsync(id);
            if (empleado == null)
                return NotFound();

            ViewBag.Descripcion = $"al empleado {empleado.Nombre} {empleado.Apellido}";
            return View("ConfirmDelete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empleadoBL.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
