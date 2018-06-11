using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Data;

namespace AirportCore.Controllers
{
    public class AirportsController : Controller
    {
        private readonly IDatabase _database;

        public AirportsController(IDatabase database)
        {
            _database = database;
        }

        //[Route("/Airports/Index")]
        public IActionResult Index()
        {
            return View(_database.GetAllAirports());
        }
    }
}
