using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalEntriesViewModel : BaseViewModel
    {
        public Entry Entry { get; set; }
        public Journal Journal { get; set; }
        public EntryVersion EntryVersion { get; set; }

        public IDataStore<Entry> EntryDataStore;
        public Command LoadEntriesCommand { get; set; }
        public ObservableCollection<Entry> Entries { get; set; }

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
