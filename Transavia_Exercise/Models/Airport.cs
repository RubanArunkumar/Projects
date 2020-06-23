using AirportData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transavia_Exercise.Funtions;

namespace Transavia_Exercise.Models
{
    public class Airport
    {
        public string DataFrom { get; set; }
        public PaginatedList<AirportDetails> AirportDetails { get; set; }
    }
}
