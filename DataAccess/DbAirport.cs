using System;

namespace DataAccess
{
    public class DbAirport
    {
        public Guid Id { get; set; }
        public string UserFriendlyName { get; set; }
        public string InternalName { get; set; }
        public string CityName { get; set; }
        public string IATACode { get; set; }
        public DbLocation Location { get; set; }

        public DbAirport(Guid id, string userFriendlyName, string internalName, string cityName, string iataCode, DbLocation location)
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
