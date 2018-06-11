using System;

namespace DataAccess
{
    public class DbFlight
    {
        public Guid Id { get; set; }
        public DbAirport Departure { get; set; }
        public DbAirport Destination { get; set; }
        public double AircraftFuelConsumptionLitersPerKm { get; set; }
        public double AircraftFuelConsumptionTakeoffEffort { get; set; }

        public DbFlight(Guid id, DbAirport departure, DbAirport destination, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort)
        {
            Id = id;
            Departure = departure;
            Destination = destination;
            AircraftFuelConsumptionLitersPerKm = aircraftFuelConsumptionLitersPerKm;
            AircraftFuelConsumptionTakeoffEffort = aircraftFuelConsumptionTakeoffEffort;
        }
    }
}
