using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class EntryHistoryViewModel : BaseViewModel
    {
        public IDataStore<Entry> EntryDataStore => DependencyService.Get<IDataStore<Entry>>() ?? new EntryDataStore();
        public ObservableCollection<Entry> Entries { get; set; }
        public Command LoadEntryHistoryCommand { get; set; }

        public EntryHistoryViewModel()
        {
            Title = "Entry History";
            Entries = new ObservableCollection<Entry>();
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
                var entries = await EntryDataStore.GetAllAsync(true);
				foreach (var entry in entries)
				{
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
