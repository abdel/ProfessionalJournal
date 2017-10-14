using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class EntryDetailViewModel : BaseViewModel
    {
        public bool editMode;
        public Entry Entry { get; set; }

        public EntryDetailViewModel(Entry entry = null, bool editMode = true)
        {
            Entry = entry;
            Title = entry?.Title;
            this.editMode = editMode;

            SubscribeToMessages();
        }

        public void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<EditEntryPage, Entry>(this, "EditEntry", (obj, editedEntry) =>
            {
                var _entry = editedEntry as Entry;

                Entry = _entry;
                OnPropertyChanged("Entry");
            });
        }

        public void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<EditEntryPage, Entry>(this, "EditEntry");
        }
    }
}
