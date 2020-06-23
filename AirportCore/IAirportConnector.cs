using AirportCore.Models;
using AirportData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportCore
{
    public interface IAirportConnector
    {
        Task<AirportResponse> GetAllAirports(string iso = null);
        bool addAirportinDatabase(AirportDetails airportDetails);

        void addAirportDetailsinDatabase(IEnumerable<AirportDetails> airportlists);

        void clearDatabase();
    }
}
