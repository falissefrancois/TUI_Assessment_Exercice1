using System;

namespace BusinessLogic.Managers.Interfaces
{
    public interface IFlightsManager
    {
        void CreateNewFlight(string departureAirportInternalName, string arrivalAirportInternalName, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort);
        void EditFlight(Guid flightId, string newFlightDepartureAirport, string newFlightArrivalAirport, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort);
        double GetTotalFuelConsumption(Guid id);
    }
}