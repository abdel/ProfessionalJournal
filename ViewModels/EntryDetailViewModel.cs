using System;

namespace ProfessionalJournal
{
    public class EntryDetailViewModel : BaseViewModel
    {
        public Entry Entry { get; set; }

        public EntryDetailViewModel(Entry entry = null)
        {
            Title = entry?.Title;
            Entry = entry;
        }
    }
}
