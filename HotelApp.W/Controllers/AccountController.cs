using HotelApp.W.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class AccountController : Controller
    {
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            if (model.NombreUsuario == "admin" &&
                model.Clave == "123456")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(model);
        }

   
        public IActionResult Profile()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.Mensaje = "Contraseña actualizada correctamente.";
            return View();
        }

      
        public IActionResult AccessDenied()
        {
            return View();
        }

      
        public IActionResult Logout()
        {
            return View();
        }
    }
}
