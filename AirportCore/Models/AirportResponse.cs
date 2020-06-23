using System;
using System.Collections.Generic;
using System.Text;

namespace AirportCore.Models
{
    public class AirportResponse
    {
        public string SourceFrom { get; set; }
        public IEnumerable<AirportData.Models.AirportDetails> Airports { get; set; }
    }
}
