using HotelApp.W.Models;
using HotelManagement.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelApp.W.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReservaBL _reservaBL;
        private readonly IHabitacionBL _habitacionBL;
        private readonly IClienteBL _clienteBL;
        private readonly IEmpleadoBL _empleadoBL;

        public HomeController(IReservaBL reservaBL, IHabitacionBL habitacionBL, IClienteBL clienteBL, IEmpleadoBL empleadoBL)
        {
            _reservaBL = reservaBL;
            _habitacionBL = habitacionBL;
            _clienteBL = clienteBL;
            _empleadoBL = empleadoBL;
        }

        /// <summary>Panel de inicio con indicadores del hotel y las próximas reservas.</summary>
        public async Task<IActionResult> Index()
        {
            var reservas = await _reservaBL.ObtenerTodosAsync();
            var habitaciones = await _habitacionBL.GetAllAsync();

            var modelo = new InicioViewModel
            {
                Reservas = reservas.Count,
                Habitaciones = habitaciones.Count,
                HabitacionesDisponibles = habitaciones.Count(h => h.Disponible),
                Clientes = (await _clienteBL.GetAllAsync()).Count,
                Empleados = (await _empleadoBL.GetAllAsync()).Count,
                IngresosReservados = reservas.Sum(r => r.Total),
                ProximasReservas = reservas
                    .Where(r => r.FechaSalida.Date >= DateTime.Today)
                    .OrderBy(r => r.FechaEntrada)
                    .Take(5)
                    .ToList(),
            };

            return View(modelo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
