using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.Data;
using Newtonsoft.Json;

namespace DataAccess
{
    public class Database : IDatabase
    {
        private readonly IMapper _mapper;

        private List<DbAirport> DbAirportsList { get; set; }
        private List<DbFlight> DbFlightsList { get; set; }

        private static string AirportsFilePath => Path.Combine(Directory.GetCurrentDirectory(), "airports.json");
        private static string FlightsFilePath => Path.Combine(Directory.GetCurrentDirectory(), "flights.json");

        public Database(IMapper mapper)
        {
            _mapper = mapper;

            DbAirportsList = new List<DbAirport>();
            DbFlightsList = new List<DbFlight>();

            LoadAirports();
            LoadFlights();
        }

        private void SetupFakeAirportData()
        {
            DbAirportsList.Add(new DbAirport(Guid.NewGuid(), "Paris Charles de Gaulle", "paris-cdg", "Roissy", "CDG", new DbLocation(49.00972222, 2.54777778)));
            DbAirportsList.Add(new DbAirport(Guid.NewGuid(), "Paris Orly", "paris-orly", "Orly", "ORY", new DbLocation(48.72333333, 2.37944444)));
            DbAirportsList.Add(new DbAirport(Guid.NewGuid(), "Nice Côte d'Azur", "nice-cote-d-azur", "Nice", "NCE", new DbLocation(43.66527778, 7.21500000)));
            DbAirportsList.Add(new DbAirport(Guid.NewGuid(), "Lyon–Saint Exupéry", "lyon-saint-exupery", "Lyon", "LYS", new DbLocation(45.72555556, 5.08111111)));
            DbAirportsList.Add(new DbAirport(Guid.NewGuid(), "Marseille Provence", "marseille-provence", "Marseille", "MRS", new DbLocation(43.43666667, 5.21500000)));
        }

        #region Airports
        public IEnumerable<Airport> GetAllAirports()
        {
            return _mapper.Map<IEnumerable<Airport>>(DbAirportsList);
        }

        public IEnumerable<Airport> GetAirportsById(IEnumerable<Guid> airportIds)
        {
            return _mapper
                .Map<IEnumerable<Airport>>(DbAirportsList)
                .Where(a => airportIds.Contains(a.Id));
        }
        #endregion

        #region Flights
        public IEnumerable<Flight> GetAllFlights()
        {
            return DbFlightsList
                .Select(f => _mapper.Map<DbFlight, Flight>(f));
        }

        public IEnumerable<Flight> GetFlightsById(IEnumerable<Guid> flightIds)
        {
            return DbFlightsList
                .Where(f => flightIds.Contains(f.Id))
                .Select(f => _mapper.Map<DbFlight, Flight>(f));
        }

        public void CreateNewFlight(string departureAirportInternalName, string arrivalAirportInternalName, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort)
        {
            var departureDbAirport = DbAirportsList.SingleOrDefault(a => a.InternalName == departureAirportInternalName);
            var arrivalDbAirport = DbAirportsList.SingleOrDefault(a => a.InternalName == arrivalAirportInternalName);

            if (departureDbAirport == null)
            {
                //Log
                Console.WriteLine($"Departure airport \"{departureAirportInternalName}\" not present in database");
                throw new InvalidOperationException($"Departure airport \"{departureAirportInternalName}\" not present in database");
            }

            if (arrivalDbAirport == null)
            {
                //Log
                Console.WriteLine($"Departure airport \"{arrivalAirportInternalName}\" not present in database");
                throw new InvalidOperationException($"Departure airport \"{arrivalAirportInternalName}\" not present in database");
            }

            try
            {
                DbFlightsList
                    .Add(new DbFlight(
                        Guid.NewGuid(),
                        departureDbAirport,
                        arrivalDbAirport,
                        aircraftFuelConsumptionLitersPerKm,
                        aircraftFuelConsumptionTakeoffEffort));

                SaveFlights();
            }
            catch (Exception ex)
            {
                //Log it
                Console.WriteLine(ex);
                throw ex;
            }
        }

        public void EditFlight(Guid flightId, string departureAirportInternalName, string arrivalAirportInternalName, double aircraftFuelConsumptionLitersPerKm, double aircraftFuelConsumptionTakeoffEffort)
        {
            var dbFlight = DbFlightsList.SingleOrDefault(f => f.Id == flightId);
            var departureDbAirport = DbAirportsList.SingleOrDefault(a => a.InternalName == departureAirportInternalName);
            var arrivalDbAirport = DbAirportsList.SingleOrDefault(a => a.InternalName == arrivalAirportInternalName);

            if (dbFlight == null)
            {
                //Log
                Console.WriteLine($"Flight \"{flightId}\" not present in database");
                throw new InvalidOperationException($"Flight \"{flightId}\" not present in database");
            }

            if (departureDbAirport == null)
            {
                //Log
                Console.WriteLine($"Departure airport \"{departureAirportInternalName}\" not present in database");
                throw new InvalidOperationException($"Departure airport \"{arrivalAirportInternalName}\" not present in database");
            }

            if (arrivalDbAirport == null)
            {
                //Log
                Console.WriteLine($"Departure airport \"{arrivalAirportInternalName}\" not present in database");
                throw new InvalidOperationException($"Departure airport \"{arrivalAirportInternalName}\" not present in database");
            }

            try
            {
                dbFlight.Departure = departureDbAirport;
                dbFlight.Destination = arrivalDbAirport;
                dbFlight.AircraftFuelConsumptionLitersPerKm = aircraftFuelConsumptionLitersPerKm;
                dbFlight.AircraftFuelConsumptionTakeoffEffort = aircraftFuelConsumptionTakeoffEffort;

                SaveFlights();
            }
            catch (Exception ex)
            {
                //Log it
                Console.WriteLine(ex);
                throw;
            }
        }
        #endregion

        #region File Handling
        private void LoadAirports()
        {
            if (File.Exists(AirportsFilePath))
                DbAirportsList = JsonConvert.DeserializeObject<List<DbAirport>>(File.ReadAllText(AirportsFilePath));
            else
            {
                SetupFakeAirportData();
                SaveAirports();
            }
        }

        private void SaveAirports()
        {
            File.WriteAllText(AirportsFilePath, JsonConvert.SerializeObject(DbAirportsList));
        }

        private void SaveFlights()
        {
            File.WriteAllText(FlightsFilePath, JsonConvert.SerializeObject(DbFlightsList));
        }

        //private void SaveFlight() { }

        private void LoadFlights()
        {
            if (File.Exists(FlightsFilePath))
                DbFlightsList = JsonConvert.DeserializeObject<List<DbFlight>>(File.ReadAllText(FlightsFilePath));
        }
        #endregion
    }
}
