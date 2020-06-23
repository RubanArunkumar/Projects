using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AirportData.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportData.Repository
{
    public class AirportRepository:IAirportRepository
    {
        private readonly AirportDbContext _dbContext;

        public AirportRepository(AirportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AirportDetails>> GetAsync()
        {
          return await  _dbContext.AirportDetails.ToListAsync();
        }

        public async Task AddAsync(IEnumerable<AirportDetails> europeAirports)
        {
            List<string> iataList = new List<string>();
            foreach(var item in europeAirports)
            {
               
                if (!iataList.Contains(item.Iata))
                {
                    _dbContext.AirportDetails.Add(item);
                    iataList.Add(item.Iata);
                }
               
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(AirportDetails europeAirport)
        {
                _dbContext.AirportDetails.Add(europeAirport);
                 await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync()
        {
            if (_dbContext.AirportDetails.Any())
            {
                _dbContext.AirportDetails.RemoveRange(_dbContext.AirportDetails.AsEnumerable());
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AirportDetails>> GetCountryAsync(string iso)
        {
            var results = await _dbContext.AirportDetails.ToListAsync();
            return results.Where(x => x.Iso.ToUpper() == iso.ToUpper()).ToList(); ;
        }
    }
}
