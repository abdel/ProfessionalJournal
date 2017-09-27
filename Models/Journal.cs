using System;

using Newtonsoft.Json;

namespace ProfessionalJournal
{
    public class Journal
    {
        [JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("author_id")]
		public string AuthorId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
    }
}
