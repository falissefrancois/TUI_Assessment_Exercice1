using System;
using System.Collections.Generic;

namespace BusinessLogic.Data
{
    public interface IDatabase
    {
        //Airports
        IEnumerable<Airport> GetAllAirports();
        IEnumerable<Airport> GetAirportsById(IEnumerable<Guid> airportIds);

        //Flights
        IEnumerable<Flight> GetAllFlights();
        IEnumerable<Flight> GetFlightsById(IEnumerable<Guid> flightIds);
        void EditFlight(Guid flightId, string departureAirportInternalName, string arrivalAirportInternalName, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort);
        void CreateNewFlight(string departureAirportInternalName, string arrivalAirportInternalName, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort);
    }
}