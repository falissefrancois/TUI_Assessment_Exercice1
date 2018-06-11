using System;
using System.Linq;
using BusinessLogic.Data;
using BusinessLogic.Managers.Interfaces;

namespace BusinessLogic.Managers
{
    public class FlightsManager : IFlightsManager
    {
        private readonly IDatabase _database;

        public FlightsManager(IDatabase database)
        {
            _database = database;
        }

        public void CreateNewFlight(string departureAirportInternalName, string arrivalAirportInternalName, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort)
        {
            _database.CreateNewFlight(
                departureAirportInternalName,
                arrivalAirportInternalName,
                aircraftFuelConsumptionLitersPerKm,
                aircraftFuelConsumptionTakeoffEffort);
        }

        public void EditFlight(Guid flightId, string newFlightDepartureAirport, string newFlightArrivalAirport, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort)
        {
            _database.EditFlight(flightId, newFlightDepartureAirport, newFlightArrivalAirport, aircraftFuelConsumptionLitersPerKm, aircraftFuelConsumptionTakeoffEffort);
        }

        public double GetTotalFuelConsumption(Guid idFlight)
        {
            var flight = _database.GetFlightsById(new[] { idFlight }).SingleOrDefault();
            if (flight == null)
            {
                Console.WriteLine($"Flight {idFlight} not present in database");
                throw new InvalidOperationException($"Flight {idFlight} not present in database");
            }

            return flight.AircraftFuelConsumptionTakeoffEffort + ComputeInFlightFuelConsumption(flight);
        }

        #region Private methods
        private static double ComputeInFlightFuelConsumption(Flight flight)
        {
            return flight.AircraftFuelConsumptionLitersPerKm * ComputeDistanceInKilometers(flight);
        }

        private static double ComputeDistanceInKilometers(Flight flight)
        {
            return GeoUtils.Haversine(
                flight.Departure.Location.Latitude,
                flight.Departure.Location.Longitude,
                flight.Destination.Location.Latitude,
                flight.Destination.Location.Longitude);
        }
        #endregion
    }
}
