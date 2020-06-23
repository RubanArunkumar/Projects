using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AirportData.Models
{
    public class AirportDetails
    {
        [Key]
        [JsonProperty(PropertyName = "iata")]
        public string Iata { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public string Longitude { get; set; }

        [JsonProperty(PropertyName = "iso")]
        public string Iso { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "continent")]
        public string Continent { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }
    }

    public enum HeaderType
    {
        Cache,
        Database
    }
}
