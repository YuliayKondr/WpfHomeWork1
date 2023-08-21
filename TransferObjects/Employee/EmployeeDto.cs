using Newtonsoft.Json;

namespace TransferObjects.Employee
{
    public sealed record EmployeeDto
    {
        [JsonProperty("id")]
        public int Id { get; init; }

        [JsonProperty("uid")]
        public string Uid { get; init; }

        [JsonProperty("password")]
        public string Password { get; init; }

        [JsonProperty("first_name")]
        public string Name { get; init; }

        [JsonProperty("last_name")]
        public string LastName { get; init; }

        [JsonProperty("username")]
        public string Username { get; init; }

        [JsonProperty("email")]
        public string Email { get; init; }

        [JsonProperty("avatar")]
        public string Avatar { get; init; }

        [JsonProperty("gender")]
        public string Gender { get; init; }

        [JsonProperty("phone_number")]
        public string PhoneNumer { get; init; }

        [JsonProperty("social_insurance_number")]
        public string SocialInsuranceNumber { get; init; }

        [JsonProperty("date_of_birth")]
        public DateTime DateOfBirth { get; init; }

        [JsonProperty("employment")]
        public EmploymentDto Employment { get; init; }

        [JsonProperty("address")]
        public AddressDto Address { get; init; }

        [JsonProperty("credit_card")]
        public CreditCardDto CreditCard { get; init; }

        [JsonProperty("subscription")]
        public SubscriptionDto Subscription { get; init; }
    }
}