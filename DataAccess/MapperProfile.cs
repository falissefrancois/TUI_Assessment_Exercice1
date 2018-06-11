using AutoMapper;
using BusinessLogic;

namespace DataAccess
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Location, DbLocation>();
            CreateMap<DbLocation, Location>();

            CreateMap<Flight, DbFlight>();
            CreateMap<DbFlight, Flight>();

            CreateMap<Airport, DbAirport>();
            CreateMap<DbAirport, Airport>();
        }
    }
}
