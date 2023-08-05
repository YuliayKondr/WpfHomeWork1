using Newtonsoft.Json;

namespace TransferObjects.Employee
{
    public sealed record CreditCardDto
    {
        [JsonProperty("cc_number")]
        public string CcNumber {  get; init; }
    }
}
