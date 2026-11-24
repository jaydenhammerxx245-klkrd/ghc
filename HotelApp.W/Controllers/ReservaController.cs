using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaBL _reservaBL;

        public ReservaController(IReservaBL reservaBL)
        {
            _reservaBL = reservaBL;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _reservaBL.ObtenerTodosAsync();
            return View(reservas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var reserva = await _reservaBL.ObtenerPorIdAsync(id);

            if (reserva == null)
                return NotFound();

            return View(reserva);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _reservaBL.CrearAsync(reserva);
                    return RedirectToAction(nameof(Index));
                }
                catch (ReglaNegocioException ex)
                {
                    // La regla de negocio rechazó la reserva: se muestra el motivo en el formulario.
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(reserva);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _reservaBL.ObtenerPorIdAsync(id);

            if (reserva == null)
                return NotFound();

            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!await _reservaBL.ActualizarAsync(reserva))
                        return NotFound();

                    return RedirectToAction(nameof(Index));
                }
                catch (ReglaNegocioException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(reserva);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reserva = await _reservaBL.ObtenerPorIdAsync(id);

            if (reserva == null)
                return NotFound();

            ViewBag.Descripcion = $"la reserva #{reserva.IdReserva}";
            return View("ConfirmDelete", id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservaBL.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}