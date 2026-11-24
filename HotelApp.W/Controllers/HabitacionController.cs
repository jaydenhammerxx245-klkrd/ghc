using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly IHabitacionBL _habitacionBL;

        public HabitacionController(IHabitacionBL habitacionBL)
        {
            _habitacionBL = habitacionBL;
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

        public IActionResult Create()
        {
            return View();
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

            return View(habitacion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);

            if (habitacion == null)
                return NotFound();

            return View(habitacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                await _habitacionBL.UpdateAsync(habitacion);
                return RedirectToAction(nameof(Index));
            }

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
            await _habitacionBL.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
