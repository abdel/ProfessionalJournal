using System;

using Newtonsoft.Json;

namespace ProfessionalJournal
{
    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("deleted")]
        public Boolean Deleted { get; set; }

        [JsonProperty("hidden")]
        public Boolean Hidden { get; set; }
    }
}
