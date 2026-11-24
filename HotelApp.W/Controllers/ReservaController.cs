using HotelManagement.BL.Interfaces;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelApp.W.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaBL _reservaBL;
        private readonly IClienteBL _clienteBL;
        private readonly IHabitacionBL _habitacionBL;

        public ReservaController(IReservaBL reservaBL, IClienteBL clienteBL, IHabitacionBL habitacionBL)
        {
            _reservaBL = reservaBL;
            _clienteBL = clienteBL;
            _habitacionBL = habitacionBL;
        }

        // Llena las listas desplegables de clientes y habitaciones del formulario.
        private async Task CargarListasAsync(Reserva? reserva = null)
        {
            var clientes = (await _clienteBL.GetAllAsync())
                .Select(c => new { c.IdCliente, Texto = $"{c.Nombre} {c.Apellido} · {c.Cedula}" });
            var habitaciones = (await _habitacionBL.GetAllAsync())
                .Select(h => new
                {
                    h.IdHabitacion,
                    Texto = $"{h.Numero} · {(string.IsNullOrEmpty(h.Tipo) ? "Habitación" : h.Tipo)} · {h.PrecioNoche:N2} por noche"
                            + (h.Disponible ? "" : " (no disponible)"),
                });

            ViewBag.Clientes = new SelectList(clientes, "IdCliente", "Texto", reserva?.IdCliente);
            ViewBag.Habitaciones = new SelectList(habitaciones, "IdHabitacion", "Texto", reserva?.IdHabitacion);
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

        public async Task<IActionResult> Create()
        {
            await CargarListasAsync();
            return View(new Reserva { FechaEntrada = DateTime.Today, FechaSalida = DateTime.Today.AddDays(1) });
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

            await CargarListasAsync(reserva);
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _reservaBL.ObtenerPorIdAsync(id);

            if (reserva == null)
                return NotFound();

            await CargarListasAsync(reserva);
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

            await CargarListasAsync(reserva);
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