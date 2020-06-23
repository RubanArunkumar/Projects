using AirportCore;
using AirportData;
using System;
using Xunit;
using System.Net;
using AirportCore.Services;
using Moq;
using AirportCore.Service;
using Microsoft.EntityFrameworkCore;
using AirportData.Repository;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Airports.UnitTest
{
    public class UnitTest
    {
        [Fact]
        public async void GetAllAirportsReturnNotNull()
        {
            ICachingService cache = new CachingService();

            DbContextOptions<AirportDbContext> options = new DbContextOptionsBuilder<AirportDbContext>().
                UseSqlServer("data source=.;initial catalog=test_Local;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;")
                .Options;

            AirportDbContext context = new AirportDbContext(options);
            IAirportRepository airportRepository = new AirportRepository(context);
            IAirportConnector airport = new AirportConnector(cache, airportRepository);

            
            var airports = await airport.GetAllAirports();

            Assert.NotNull(airports);
        }

        [Fact]
        public async void GetAllAirportsCount()
        {
            ICachingService cache = new CachingService();

            DbContextOptions<AirportDbContext> options = new DbContextOptionsBuilder<AirportDbContext>().
                UseSqlServer("data source=.;initial catalog=test_Local;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;")
                .Options;

            AirportDbContext context = new AirportDbContext(options);
            IAirportRepository airportRepository = new AirportRepository(context);
            IAirportConnector airport = new AirportConnector(cache, airportRepository);

            int expectedValue = 1062;

            var airports = await airport.GetAllAirports();
            int actualValue = airports.Airports.ToList().Count();

            Assert.Equal(expectedValue,actualValue);
        }
    }
}
