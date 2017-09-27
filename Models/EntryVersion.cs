using System;

using Newtonsoft.Json;

namespace ProfessionalJournal
{
    public class EntryVersion
    {
        [JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("entry_id")]
		public string EntryId { get; set; }

		[JsonProperty("version_track_id")]
		public string VersionTrackId { get; set; }

        [JsonProperty("text_entry")]
        public string TextEntry { get; set; }
        
        [JsonProperty("attachment")]
        public string Attachment { get; set; }

        [JsonProperty("modify_reason")]
        public string ModifyReason { get; set; }
    }
}
