using HotelApp.W.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class EmpleadoController : Controller
    {
        private static readonly List<EmpleadoViewModel> empleados = [];

   
        public IActionResult Index()
        {
            return View(empleados);
        }
         
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmpleadoViewModel empleado)
        {
            empleado.IdEmpleado = empleados.Count == 0 ? 1 : empleados.Max(e => e.IdEmpleado) + 1;

            empleados.Add(empleado);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var empleado = empleados.FirstOrDefault(
                e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound();

            return View(empleado);
        }
        public IActionResult Details(int id)
        {
            var empleado = empleados.FirstOrDefault(
                e => e.IdEmpleado == id);

            if (empleado == null)
                return NotFound();

            return View(empleado);
        }
        [HttpPost]
        public IActionResult Edit(EmpleadoViewModel empleado)
        {
            var empleadoActual = empleados.FirstOrDefault(
                e => e.IdEmpleado == empleado.IdEmpleado);

            if (empleadoActual != null)
            {
                empleadoActual.Nombre = empleado.Nombre;
                empleadoActual.Apellido = empleado.Apellido;
                empleadoActual.Cedula = empleado.Cedula;
                empleadoActual.Telefono = empleado.Telefono;
                empleadoActual.Cargo = empleado.Cargo;
                empleadoActual.Correo = empleado.Correo;
                empleadoActual.Salario = empleado.Salario;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var empleado = empleados.FirstOrDefault(
                e => e.IdEmpleado == id);

            if (empleado != null)
                empleados.Remove(empleado);

            return RedirectToAction(nameof(Index));
        }

    }
}
