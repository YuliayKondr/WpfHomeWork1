using Newtonsoft.Json;

namespace TransferObjects.Employee
{
    public sealed record EmploymentDto
    {
        [JsonProperty("title")]
        public string Title { get; init; }

        [JsonProperty("key_skill")]
        public string KeySkill { get; init; }
    }
}
