using HotelApp.W.Models;
using HotelManagement.BL.Interfaces;
using HotelManagement.BL.Services;
using HotelManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpPost]
        public async Task<IActionResult> Create(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                await _habitacionBL.AddAsync(habitacion);
                return RedirectToAction(nameof(Index));
            }

            return View(habitacion);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                await _habitacionBL.AddAsync(habitacion);
                return RedirectToAction(nameof(Index));
            }

            return View(habitacion);
        }
         
        [HttpPost]
        public async Task<IActionResult> Edit(Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                await _habitacionBL.UpdateAsync(habitacion);
                return RedirectToAction(nameof(Index));
            }

            return View(habitacion);
        }

        public async Task<IActionResult> Details(int id)
        {
            var habitacion = await _habitacionBL.GetByIdAsync(id);
            return View(habitacion);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _habitacionBL.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}