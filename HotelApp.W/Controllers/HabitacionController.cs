using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelApp.W.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly IHabitacionBL _habitacionBL;
        private readonly IPisoBL _pisoBL;

        public HabitacionController(IHabitacionBL habitacionBL, IPisoBL pisoBL)
        {
            _habitacionBL = habitacionBL;
            _pisoBL = pisoBL;
        }

        // Llena el desplegable de pisos de los formularios Crear/Editar.
        private async Task CargarPisosAsync(int? seleccionado = null)
        {
            var pisos = await _pisoBL.ObtenerTodosAsync();
            ViewBag.Pisos = new SelectList(pisos, nameof(Piso.IdPiso), nameof(Piso.Nombre), seleccionado);
        }

        public async Task<IActionResult> Index()
        {
            var habitaciones = await _habitacionBL.GetAllAsync();
            return View(habitaciones);
        }

        public async Task<IActionResult> Details(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);

            if (habitacion == null)
                return NotFound();

            return View(habitacion);
        }

        public async Task<IActionResult> Create()
        {
            await CargarPisosAsync();
            return View(new Habitacion { Disponible = true, Capacidad = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                await _habitacionBL.AddAsync(habitacion);
                return RedirectToAction(nameof(Index));
            }

            await CargarPisosAsync(habitacion.IdPiso);
            return View(habitacion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);

            if (habitacion == null)
                return NotFound();

            await CargarPisosAsync(habitacion.IdPiso);
            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                if (!await _habitacionBL.UpdateAsync(habitacion))
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }

            await CargarPisosAsync(habitacion.IdPiso);
            return View(habitacion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);

            if (habitacion == null)
                return NotFound();

            ViewBag.Descripcion = $"la habitación {habitacion.Numero}";
            return View("ConfirmDelete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _habitacionBL.DeleteAsync(id);
            }
            catch (ReglaNegocioException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
