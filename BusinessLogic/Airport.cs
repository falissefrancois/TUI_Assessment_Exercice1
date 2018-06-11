using System;

namespace BusinessLogic
{
    public class Airport
    {
        public Guid Id { get; set; }
        public string UserFriendlyName { get; set; }
        public string InternalName { get; set; }
        public string CityName { get; set; }
        public string IATACode { get; set; }
        public Location Location { get; set; }

        public Airport()
        {
            Id = Guid.Empty;
            UserFriendlyName = string.Empty;
            InternalName = string.Empty;
            CityName = string.Empty;
            IATACode = string.Empty;
            Location = new Location(0, 0);
        }

        public Airport(Guid id, string userFriendlyName, string internalName, string cityName, string iataCode, Location location)
        {
            Id = id;
            UserFriendlyName = userFriendlyName;
            InternalName = internalName;
            CityName = cityName;
            IATACode = iataCode;
            Location = location;
        }
    }
}
