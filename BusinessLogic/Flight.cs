using System;

namespace BusinessLogic
{
    public class Flight
    {
        public Guid Id { get; set; }
        public Airport Departure { get; set; }
        public Airport Destination { get; set; }
        public double AircraftFuelConsumptionLitersPerKm { get; set; }
        public double AircraftFuelConsumptionTakeoffEffort { get; set; }

        public Flight()
        {
            Id = Guid.Empty;
            Departure = new Airport();
            Destination = new Airport();
            AircraftFuelConsumptionLitersPerKm = 0.0;
            AircraftFuelConsumptionTakeoffEffort = 0.0;
        }

        public Flight(Guid id, Airport departure, Airport destination, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort)
        {
            Id = id;
            Departure = departure;
            Destination = destination;
            AircraftFuelConsumptionLitersPerKm = aircraftFuelConsumptionLitersPerKm;
            AircraftFuelConsumptionTakeoffEffort = aircraftFuelConsumptionTakeoffEffort;
        }
    }
}
