using Microsoft.AspNetCore.Mvc;

namespace HotelApp.W.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}