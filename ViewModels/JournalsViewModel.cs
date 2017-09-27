using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalsViewModel : BaseViewModel
    {
		public IDataStore<Journal> JournalDataStore => DependencyService.Get<IDataStore<Journal>>() ?? new JournalDataStore();
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

                try
                {
					await JournalDataStore.AddAsync(_journal);
                }
				catch (Exception e)
				{
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
				}
            });
        }

        async Task ExecuteLoadJournalsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var journals = await JournalDataStore.GetAllAsync(true);

                if (journals != null && journals.Any()) {
                    Journals.Clear();
                    foreach (var journal in journals)
                    {
                        Journals.Add(journal);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                // Logout user if session expired
                if (ex.Message.Contains("Unauthorized") || ex.Message.Contains("Internal Server Error"))
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
