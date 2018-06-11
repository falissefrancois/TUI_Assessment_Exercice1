using AirportCore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirportCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[Route("/Home/Error/{statusCode}")]
        public IActionResult Error(int? statusCode = null)
        {
            return View(new ErrorViewModel { StatusCode = statusCode ?? 0 });
        }
    }
}
