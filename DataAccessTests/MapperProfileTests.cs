using AutoMapper;
using DataAccess;
using Xunit;

namespace DataAccessTests
{
    public class MapperProfileTests
    {
        [Fact]
        public void MapperProfileTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}
