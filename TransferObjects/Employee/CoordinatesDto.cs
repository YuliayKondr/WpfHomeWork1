using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObjects.Employee
{
    public sealed record CoordinatesDto
    {
        [JsonProperty("lat")]
        public decimal Lat { get; init; }

        [JsonProperty("lng")]
        public decimal Lng { get; init; }
    }
}
