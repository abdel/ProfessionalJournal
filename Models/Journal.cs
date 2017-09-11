using System;

using Newtonsoft.Json;

namespace ProfessionalJournal
{
    public class Journal
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
