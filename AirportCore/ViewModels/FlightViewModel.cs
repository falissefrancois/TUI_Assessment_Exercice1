using AirportCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirportCore.ViewModels
{
    public class FlightViewModel
    {
        public IEnumerable<FlightModel> Flights { get; set; }
        public IEnumerable<SelectListItem> AvailableAirports { get; set; }

        [Required]
        [Display(Name="Departure Airport")]
        public string NewFlightDepartureAirport { get; set; }

        [Required]
        [Display(Name="Arrival Airport")]
        public string NewFlightArrivalAirport { get; set; }

        [Required]
        [Range(0.00001, double.MaxValue, ErrorMessage = "Fuel consumption must be > 0.0")]
        [Display(Name = "Liters per KM")]
        public double NewFuelConsumptionLitersPerKm { get; set; }

        [Required]
        [Range(0.00001, double.MaxValue, ErrorMessage = "Takeoff effort must be > 0.0")]
        [Display(Name = "Takeoff Effort")]
        public double NewFuelConsumptionTakeoffEffortInLiters { get; set; }
    }
}
