using System;

using Newtonsoft.Json;

namespace ProfessionalJournal
{
    public class Author
    {
        [JsonProperty("id")]
		public string Id { get; set; }

        [JsonProperty("first_name")]
		public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("date_of_birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
