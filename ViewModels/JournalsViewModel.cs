using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalsViewModel : BaseViewModel
    {
        public ObservableCollection<Journal> Journals { get; set; }
        public Command LoadJournalsCommand { get; set; }

        public JournalsViewModel()
        {
            Title = "Journals";
            Journals = new ObservableCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            MessagingCenter.Subscribe<NewJournalPage, Journal>(this, "AddJournal", async (obj, journal) =>
            {
                var _journal = journal as Journal;
                Journals.Add(_journal);
                await DataStore.AddJournalAsync(_journal);
            });
        }

        async Task ExecuteLoadJournalsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Journals.Clear();
                var journals = await DataStore.GetJournalsAsync(true);
                foreach (var journal in journals)
                {
                    Journals.Add(journal);
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
