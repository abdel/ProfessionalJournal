using System;

namespace ProfessionalJournal
{
    public class JournalDetailViewModel : BaseViewModel
    {
        public Journal Journal { get; set; }
        public JournalDetailViewModel(Journal journal = null)
        {
            Title = journal?.Name;
            Journal = journal;
        }
    }
}
