using AirportData;
using System;
using System.Collections.Generic;
using System.Text;
using AirportData.Models;
using System.Threading.Tasks;
using AirportCore.Services;
using System.Linq;
using AirportCore.Models;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace AirportCore.Service
{
    public class AirportConnector : IAirportConnector
    {
        

        private readonly IAirportRepository _airportRepository;

        private const string euContient = "EU";

        private readonly ICachingService _cache;

        private readonly string cacheKey = "airports";

        public AirportConnector( ICachingService cache ,IAirportRepository airportRepository)
        {
            _cache=cache;
            _airportRepository = airportRepository;
        }

        public async Task<AirportResponse> GetAllAirports(string iso = null)
        {
            AirportResponse airportResponse = new AirportResponse();
            IEnumerable<AirportData.Models.AirportDetails> airports;

            if (_cache.IsCacheExists(cacheKey))
            {
                airportResponse.SourceFrom = "from-Cache";
                airports = _cache.Get<IEnumerable<AirportData.Models.AirportDetails>>(cacheKey);
            }
            else
            {
                airportResponse.SourceFrom = "from-Database";
                airports = await _airportRepository.GetAsync();
                _cache.Put(cacheKey, airports, CacheExpiryTime.FiveMinutes);
            }


            if (string.IsNullOrEmpty(iso))
                airportResponse.Airports = airports;
            else
                airportResponse.Airports = airports.Where(x=>x.Iso == iso).ToList();

            return airportResponse;
           
        }

        public bool addAirportinDatabase(AirportDetails airportDetails)
        {
            _cache.Remove(cacheKey);
            if(airportDetails.Continent.ToUpper().Equals(euContient))
            {
                _airportRepository.AddAsync(airportDetails);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void addAirportDetailsinDatabase(IEnumerable<AirportDetails> airportlists)
        {
            _airportRepository.AddAsync(airportlists);
        }

        public void clearDatabase()
        {
            _airportRepository.DeleteAsync();
        }
    }
}
