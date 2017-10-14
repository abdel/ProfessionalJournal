using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalEntriesViewModel : BaseViewModel
    {
        public bool toggleAll;
        public SearchBar searchBar;
        public DateTime[] searchDates;

        public Label listViewNoItems;
        public ListView EntriesListView;

        public Entry Entry { get; set; }
        public Journal Journal { get; set; }
        public EntryVersion EntryVersion { get; set; }

        public IDataStore<Entry> EntryDataStore;
        public Command LoadEntriesCommand { get; set; }
        public Command SearchCommand { get; private set; }
        public ObservableCollection<Entry> Entries { get; set; }

        public JournalEntriesViewModel(Journal journal = null)
        {
            Journal = journal;
            Title = journal?.Title;

            Entries = new ObservableCollection<Entry>();
            EntryDataStore = new EntryDataStore(journal?.Id);
            LoadEntriesCommand = new Command(async () => await ExecuteLoadEntriesCommand());
            SearchCommand = new Command<string>(async (text) => await ExecuteLoadEntriesCommand(text));

            SubscribeToMessages();
        }

        public void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<NewEntryPage, Entry>(this, "AddEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;
                Entries.Add(_entry);

                try
                {
                    await EntryDataStore.AddAsync(_entry);
                    LoadEntriesCommand.Execute(null);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });

            MessagingCenter.Subscribe<EditEntryPage, Entry>(this, "EditEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;

                try
                {
                    await EntryDataStore.UpdateAsync(_entry);
                    LoadEntriesCommand.Execute(null);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });

            MessagingCenter.Subscribe<JournalEntriesPage, Entry>(this, "HideEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;

                try
                {
                    Entries.Remove(_entry);
                    await EntryDataStore.HideAsync(_entry.Id);
                    LoadEntriesCommand.Execute(null);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });

            MessagingCenter.Subscribe<JournalEntriesPage, Entry>(this, "UnhideEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;

                try
                {
                    await EntryDataStore.UnhideAsync(_entry.Id);
                    LoadEntriesCommand.Execute(null);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });

            MessagingCenter.Subscribe<JournalEntriesPage, Entry>(this, "DeleteEntry", async (obj, entry) =>
            {
                var _entry = entry as Entry;

                try
                {
                    Entries.Remove(_entry);
                    await EntryDataStore.DeleteAsync(_entry.Id);
                    LoadEntriesCommand.Execute(null);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });

            MessagingCenter.Subscribe<SearchPopupPage, DateTime[]>(this, "DateSearch", async (obj, dates) =>
            {
                var _dates = dates as DateTime[];

                try
                {
                    this.searchDates = dates;
                    LoadEntriesCommand.Execute(null);
                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
            });
        }

        public void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<NewEntryPage, Entry>(this, "AddEntry");
            MessagingCenter.Unsubscribe<EditEntryPage, Entry>(this, "EditEntry");
            MessagingCenter.Unsubscribe<JournalEntriesPage, Entry>(this, "HideEntry");
            MessagingCenter.Unsubscribe<JournalEntriesPage, Entry>(this, "UnhideEntry");
            MessagingCenter.Unsubscribe<JournalEntriesPage, Entry>(this, "DeleteEntry");
            MessagingCenter.Unsubscribe<SearchPopupPage, DateTime[]>(this, "DateSearch");
        }

        public async Task ExecuteLoadEntriesCommand(string text = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            bool hidden = false;
            bool deleted = false;

            if (text == null)
            {
                text = searchBar.Text;
            }

            try
            {
                EntriesListView.IsVisible = true;
                listViewNoItems.IsVisible = false;

                if (toggleAll) {
                    hidden = true;
                    deleted = true;
                }

                var entries = await EntryDataStore.GetAllAsync(true, text, deleted, hidden, searchDates);

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
                Console.WriteLine(ex);

                if (ex.Message.Contains("No entries found")) {
                    Entries.Clear();

                    EntriesListView.IsVisible = false;
                    listViewNoItems.IsVisible = true;
                }

				// Logout user if session expired
				if (ex.Message.Contains("Unauthorized") || ex.Message.Contains("Internal Server Error"))
				{
                    this.UnsubscribeFromMessages();

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
