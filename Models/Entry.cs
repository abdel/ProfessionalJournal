using System;

namespace ProfessionalJournal
{
    public class Entry
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean Hidden { get; set; }
    }
}
