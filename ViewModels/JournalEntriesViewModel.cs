using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalEntriesViewModel : BaseViewModel
    {
        public EntryVersion EntryVersion { get; set; }
        public Journal Journal { get; set; }
        public Entry Entry { get; set; }

        public ObservableCollection<Entry> Entries { get; set; }
        public Command LoadEntriesCommand { get; set; }
        public IDataStore<Entry> EntryDataStore;

        public JournalEntriesViewModel(Journal journal = null)
        {
            Journal = journal;
            Title = journal?.Title;

            Entries = new ObservableCollection<Entry>();
            LoadEntriesCommand = new Command(async () => await ExecuteLoadEntriesCommand());
            EntryDataStore = new EntryDataStore(journal?.Id);

            MessagingCenter.Subscribe<NewEntryPage, Entry>(this, "AddEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;
                Entries.Add(_entry);

                try
                {
                    await EntryDataStore.AddAsync(_entry);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });

            MessagingCenter.Subscribe<EditEntryPage, Entry>(this, "EditEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;
                var currentEntry = Entries.FirstOrDefault(i => i.Title == _entry.Title);

                _entry.EntryVersion.EntryId = currentEntry.Id;
                _entry.EntryVersion.VersionTrackId = currentEntry.EntryVersion.VersionTrackId + 1;

                try
                {
                    await EntryDataStore.UpdateAsync(_entry);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });
        }

        async Task ExecuteLoadEntriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var entries = await EntryDataStore.GetAllAsync(true);

                if (entries != null && entries.Any())
                {
                    Entries.Clear();
                    foreach (var entry in entries)
                    {
                        Entries.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                // Logout user if session expired
                if (ex.Message == "Unauthorized")
                {
                    MessagingCenter.Send(this, "AuthorLogout");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
