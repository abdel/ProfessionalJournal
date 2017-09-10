using System;

namespace ProfessionalJournal
{
    public class JournalEntriesViewModel : BaseViewModel
    {
        public Journal Journal { get; set; }
        public JournalEntriesViewModel(Journal journal = null)
        {
            Title = journal?.Title;
            Journal = journal;
        }
    }
}
