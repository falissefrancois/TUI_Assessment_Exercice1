using BusinessLogic;

namespace AirportCore.Models
{
    public class FlightModel
    {
        public string Id { get; set; }
        public Airport Departure { get; set; }
        public Airport Destination { get; set; }
        public double AircraftFuelConsumptionLitersPerKm { get; set; }
        public double AircraftFuelConsumptionTakeoffEffort { get; set; }
        public readonly string FuelConsumptionDetails;

        public FlightModel(string id, Airport departure, Airport destination, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort, string fuelConsumptionDetails)
        {
            Id = id;
            Departure = departure;
            Destination = destination;
            FuelConsumptionDetails = fuelConsumptionDetails;
            AircraftFuelConsumptionLitersPerKm = aircraftFuelConsumptionLitersPerKm;
            AircraftFuelConsumptionTakeoffEffort = aircraftFuelConsumptionTakeoffEffort;
        }
    }
}
