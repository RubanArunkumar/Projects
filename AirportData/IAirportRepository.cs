using AirportData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportData
{
    public interface IAirportRepository
    {
        Task<IEnumerable<AirportDetails>> GetAsync();

        Task<IEnumerable<AirportDetails>> GetCountryAsync(string iso);

        Task AddAsync(AirportDetails europeAirport);

        Task AddAsync(IEnumerable<AirportDetails> europeAirports);

        Task DeleteAsync();
    }
}
