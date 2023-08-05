using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObjects.Employee
{
    public sealed record SubscriptionDto
    {
        [JsonProperty("plan")]
        public string Plan { get; init; }

        [JsonProperty("status")]
        public string Status { get; init; }

        [JsonProperty("payment_method")]
        public string PaymentMethod { get; init; }

        [JsonProperty("term")]
        public string Term { get; init; }
    }
}
