using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using AirportCore.ViewModels;
using BusinessLogic.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirportCore.Models;
using BusinessLogic.Data;

namespace AirportCore.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightsManager _flightsManager;
        private readonly IDatabase _database;

        public FlightsController(IFlightsManager flightsManager, IDatabase database)
        {
            _flightsManager = flightsManager;
            _database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(CreateDefaultFlightViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewFlight(FlightViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", CreateDefaultFlightViewModel(model.NewFlightDepartureAirport, model.NewFlightArrivalAirport, model.NewFuelConsumptionLitersPerKm, model.NewFuelConsumptionTakeoffEffortInLiters));

            if (model.NewFlightDepartureAirport == model.NewFlightArrivalAirport)
            {
                ModelState.AddModelError("NewFlightArrivalAirport", "Departure and destination airports can't be the same !");
                return View("Index", CreateDefaultFlightViewModel(model.NewFlightDepartureAirport, model.NewFlightArrivalAirport, model.NewFuelConsumptionLitersPerKm, model.NewFuelConsumptionTakeoffEffortInLiters));
            }

            try
            {
                _flightsManager.CreateNewFlight(model.NewFlightDepartureAirport, model.NewFlightArrivalAirport, model.NewFuelConsumptionLitersPerKm, model.NewFuelConsumptionTakeoffEffortInLiters);
            }
            catch (Exception)
            {
                ModelState.AddModelError("NewFuelConsumptionTakeoffEffortInLiters", "An error occured");
                return View("Index", CreateDefaultFlightViewModel(model.NewFlightDepartureAirport, model.NewFlightArrivalAirport, model.NewFuelConsumptionLitersPerKm, model.NewFuelConsumptionTakeoffEffortInLiters));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/[Action]/{flightId}")]
        public IActionResult EditFlight(string flightId)
        {
            return View(CreateDefaultEditFlightViewModel(flightId));
        }

        [HttpPost("[controller]/[Action]/{flightId}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditFlight(FlightViewModel model, string flightId)
        {
            if (!ModelState.IsValid)
                return View("EditFlight", CreateDefaultEditFlightViewModel(flightId));

            if (model.NewFlightDepartureAirport == model.NewFlightArrivalAirport)
            {
                ModelState.AddModelError("NewFlightArrivalAirport", "Departure and destination airports can't be the same !");
                return View("EditFlight", CreateDefaultEditFlightViewModel(flightId));
            }

            try
            {
                _flightsManager.EditFlight(Guid.Parse(flightId), model.NewFlightDepartureAirport, model.NewFlightArrivalAirport, model.NewFuelConsumptionLitersPerKm, model.NewFuelConsumptionTakeoffEffortInLiters);
            }
            catch (Exception)
            {
                ModelState.AddModelError("NewFlightArrivalAirport", "An error occured");
                return View("EditFlight", CreateDefaultEditFlightViewModel(flightId));
            }

            return RedirectToAction(nameof(Index));
        }

        private FlightViewModel CreateDefaultEditFlightViewModel(string flightId)
        {
            var flight = _database.GetFlightsById(new[] { Guid.Parse(flightId) }).Single();

            return new FlightViewModel
            {
                Flights = new[]
                {
                    new FlightModel(
                        flight.Id.ToString(),
                        flight.Departure,
                        flight.Destination,
                        flight.AircraftFuelConsumptionLitersPerKm,
                        flight.AircraftFuelConsumptionTakeoffEffort,
                        $"{_flightsManager.GetTotalFuelConsumption(flight.Id):0.###} Liters")
                },

                AvailableAirports = _database.GetAllAirports()
                    .Select(a => new SelectListItem { Value = a.InternalName, Text = a.UserFriendlyName }),

                NewFlightDepartureAirport = flight.Departure.InternalName,
                NewFlightArrivalAirport = flight.Destination.InternalName,

                NewFuelConsumptionLitersPerKm = flight.AircraftFuelConsumptionLitersPerKm,
                NewFuelConsumptionTakeoffEffortInLiters = flight.AircraftFuelConsumptionTakeoffEffort,
            };
        }

        private FlightViewModel CreateDefaultFlightViewModel(string newFlightDepartureAirport = "", string newFlightArrivalAirport = "", double newFuelConsumptionLitersPerKm = 0.0, double newFuelConsumptionTakeoffEffortInLiters = 0.0)
        {
            var test = _database.GetAllFlights()
                .Select(f => new FlightModel(
                    f.Id.ToString(),
                    f.Departure,
                    f.Destination,
                    f.AircraftFuelConsumptionLitersPerKm,
                    f.AircraftFuelConsumptionTakeoffEffort,
                    $"{_flightsManager.GetTotalFuelConsumption(f.Id):0.###} Liters"));


            return new FlightViewModel
            {
                Flights = _database.GetAllFlights()
                        .Select(f => new FlightModel(
                            f.Id.ToString(),
                            f.Departure,
                            f.Destination,
                            f.AircraftFuelConsumptionLitersPerKm,
                            f.AircraftFuelConsumptionTakeoffEffort,
                            $"{_flightsManager.GetTotalFuelConsumption(f.Id):0.###} Liters")),

                AvailableAirports = _database.GetAllAirports()
                    .Select(a => new SelectListItem { Value = a.InternalName, Text = a.UserFriendlyName }),

                NewFlightDepartureAirport = newFlightDepartureAirport,
                NewFlightArrivalAirport = newFlightArrivalAirport,

                NewFuelConsumptionLitersPerKm = newFuelConsumptionLitersPerKm,
                NewFuelConsumptionTakeoffEffortInLiters = newFuelConsumptionTakeoffEffortInLiters
            };
        }
    }
}
