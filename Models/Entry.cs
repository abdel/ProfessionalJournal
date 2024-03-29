﻿using System;

using Newtonsoft.Json;

namespace ProfessionalJournal
{
    public class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("journal_id")]
		public string JournalId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("deleted")]
        public Boolean Deleted { get; set; }

        [JsonProperty("hidden")]
        public Boolean Hidden { get; set; }

        [JsonProperty("entry_version")]
        public EntryVersion EntryVersion { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
    }
}
