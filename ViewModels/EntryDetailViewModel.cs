using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class EntryDetailViewModel : BaseViewModel
    {
        public Entry Entry { get; set; }

        public EntryDetailViewModel(Entry entry = null)
        {
            Title = entry?.Title;
            Entry = entry;

            MessagingCenter.Subscribe<EditEntryPage, Entry>(this, "EditEntry", (obj, editedEntry) =>
            {
                var _entry = editedEntry as Entry;

                Entry = _entry;
                OnPropertyChanged("Entry");
            });
        }
    }
}
