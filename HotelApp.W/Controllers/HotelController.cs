using HotelApp.W.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelApp.W.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
