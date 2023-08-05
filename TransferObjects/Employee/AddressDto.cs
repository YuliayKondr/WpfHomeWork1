using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObjects.Employee
{
    public sealed record AddressDto
    {
        [JsonProperty("city")]
        public string City { get; init; }

        [JsonProperty("street_name")]
        public string StreetName { get; init; }

        [JsonProperty("street_address")]
        public string StreetAddress { get; init; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; init; }

        [JsonProperty("state")]
        public string State { get; init; }

        [JsonProperty("country")]
        public string Country { get; init; }

        [JsonProperty("coordinates")]
        public CoordinatesDto Coordinates { get; init; }
    }
}
