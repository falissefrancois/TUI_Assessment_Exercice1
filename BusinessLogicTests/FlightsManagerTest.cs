using System;
using BusinessLogic;
using BusinessLogic.Data;
using BusinessLogic.Managers;
using BusinessLogic.Managers.Interfaces;
using Moq;
using Xunit;

namespace BusinessLogicTests
{
    public class FlightsManagerTest
    {
        readonly IFlightsManager _flightsManager;
        readonly Mock<IDatabase> _databaseMock;

        readonly Guid _testFlightId = Guid.NewGuid();
        readonly Airport _departureAirport = new Airport(Guid.NewGuid(), "test Departure", "testDeparture", "testCity", "TST", new Location(5.0, 10.0));
        readonly Airport _destinationAirport = new Airport(Guid.NewGuid(), "test Departure", "testDeparture", "testCity", "TST", new Location(20.0, 25.0));
        readonly Flight _testFlight;

        public FlightsManagerTest()
        {
            _databaseMock = new Mock<IDatabase>();

            _testFlight = new Flight(_testFlightId, _departureAirport, _destinationAirport, 2.4, 26.3);

            _flightsManager = new FlightsManager(_databaseMock.Object);
        }

        [Fact]
        public void WhenCreatingNewFlight_GivenFlightParameters_ShouldCallCreateFlightCorrectly()
        {
            _databaseMock.Setup(d => d.CreateNewFlight(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>())).Verifiable();

            _flightsManager.CreateNewFlight(
                _testFlight.Departure.InternalName,
                _testFlight.Destination.InternalName,
                _testFlight.AircraftFuelConsumptionLitersPerKm,
                _testFlight.AircraftFuelConsumptionTakeoffEffort);

            _databaseMock.Verify(d => d.CreateNewFlight(
                _testFlight.Departure.InternalName,
                _testFlight.Destination.InternalName,
                _testFlight.AircraftFuelConsumptionLitersPerKm,
                _testFlight.AircraftFuelConsumptionTakeoffEffort),
                Times.Once);
        }

        [Fact]
        public void WhenEditingFlight_GivenFlightParameters_ShouldCallEditFlightCorrectly()
        {
            _databaseMock.Setup(d => d.EditFlight(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>())).Verifiable();

            _flightsManager.EditFlight(
                _testFlight.Id,
                _testFlight.Departure.InternalName,
                _testFlight.Destination.InternalName,
                _testFlight.AircraftFuelConsumptionLitersPerKm,
                _testFlight.AircraftFuelConsumptionTakeoffEffort);

            _databaseMock.Verify(d => d.EditFlight(
                _testFlight.Id,
                _testFlight.Departure.InternalName,
                _testFlight.Destination.InternalName,
                _testFlight.AircraftFuelConsumptionLitersPerKm,
                _testFlight.AircraftFuelConsumptionTakeoffEffort),
                Times.Once);
        }

        [Fact]
        public void WhenGettingFuelConsumptionForFlight_GivenFlight_ShouldComputeCorrectFuelRequired()
        {
            //https://www.movable-type.co.uk/scripts/latlong.html
            //https://gps-coordinates.org/distance-between-coordinates.php
            //Distance computed for '5.0, 10.0' -> '20.0, 25.0'
            var expectedDistanceInKm = 2327.12;
            var expectedInFlightFuelConsumption = expectedDistanceInKm * _testFlight.AircraftFuelConsumptionLitersPerKm;
            var expectedTotalFuelconsumption = expectedInFlightFuelConsumption + _testFlight.AircraftFuelConsumptionTakeoffEffort;

            _databaseMock.Setup(d => d.GetFlightsById(It.Is<Guid[]>(t => t[0] == _testFlightId))).Returns(new[] { _testFlight });

            var totalFuelConsumption = _flightsManager.GetTotalFuelConsumption(_testFlightId);

            Assert.Equal(expectedTotalFuelconsumption, totalFuelConsumption, 1);
        }
    }
}
