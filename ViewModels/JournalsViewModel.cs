using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalsViewModel : BaseViewModel
    {
        public ContentView JournalsNotFound;
        public ListView JournalsListView;

        public IDataStore<Journal> JournalDataStore => DependencyService.Get<IDataStore<Journal>>() ?? new JournalDataStore();
        public ObservableCollection<Journal> Journals { get; set; }
        public Command LoadJournalsCommand { get; set; }

        public JournalsViewModel()
        {
            Title = "Journals";
            Journals = new ObservableCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            SubscribeToMessages();
        }

        public void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<LoginPage>(this, "AuthorLogin", (obj) =>
            {
                JournalDataStore.ResetClient();
            });

            MessagingCenter.Subscribe<NewJournalPage, Journal>(this, "AddJournal", async (obj, journal) =>
            {
                var _journal = journal as Journal;
                Journals.Add(_journal);

                try
                {
                    await JournalDataStore.AddAsync(_journal);
                    LoadJournalsCommand.Execute(null);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("AddJournalMessage" + e);

                    if (!e.Message.Contains("A task was canceled"))
                    {
                        await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                    }
                }
            });
        }

        public void UnsubscribeFromMessages()
        {
            MessagingCenter.Unsubscribe<NewJournalPage, Journal>(this, "AddJournal");
        }

        async Task ExecuteLoadJournalsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                JournalsListView.IsVisible = true;
                JournalsNotFound.IsVisible = false;

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
                Debug.WriteLine("ExecuteLoadJournalsCommand" + ex);

                if (ex.Message.Contains("No journals found"))
                {
                    Journals.Clear();

                    JournalsListView.IsVisible = false;
                    JournalsNotFound.IsVisible = true;
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
