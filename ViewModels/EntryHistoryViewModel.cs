using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class EntryHistoryViewModel : BaseViewModel
    {
        string entryId;
        public IDataStore<Entry> EntryDataStore;
        public ObservableCollection<Entry> Entries { get; set; }
        public Command LoadEntryHistoryCommand { get; set; }

        public EntryHistoryViewModel(string entryId)
        {
            this.entryId = entryId;
            Title = "Entry History";
            Entries = new ObservableCollection<Entry>();
            EntryDataStore = DependencyService.Get<IDataStore<Entry>>() ?? new EntryDataStore(entryId);

            LoadEntryHistoryCommand = new Command(async () => await ExecuteLoadEntryHistoryCommand());
        }

        async Task ExecuteLoadEntryHistoryCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Entries.Clear();
                var entries = await EntryDataStore.GetHistoryAsync(entryId, true);
				foreach (var entry in entries)
				{
                    Console.WriteLine(entry);
					Entries.Add(entry);
				}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
