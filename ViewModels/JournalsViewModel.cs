﻿using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public class JournalsViewModel : BaseViewModel
    {
        public Label listViewNoItems;
        public ListView JournalsListView;

        public IDataStore<Journal> JournalDataStore => DependencyService.Get<IDataStore<Journal>>() ?? new JournalDataStore();
        public ObservableCollection<Journal> Journals { get; set; }
        public Command LoadJournalsCommand { get; set; }

        public JournalsViewModel()
        {
            Title = "Journals";
            Journals = new ObservableCollection<Journal>();
            LoadJournalsCommand = new Command(async () => await ExecuteLoadJournalsCommand());

            MessagingCenter.Subscribe<LoginPage>(this, "AuthorLogin", (obj) =>
            {
                JournalDataStore.ResetClient();
            });

            MessagingCenter.Unsubscribe<string>(this, "AddJournal");
            MessagingCenter.Subscribe<NewJournalPage, Journal>(this, "AddJournal", async (obj, journal) =>
            {
                var _journal = journal as Journal;
                Journals.Add(_journal);

                Console.WriteLine("AddJournal: Triggered");

                try
                {
                    await JournalDataStore.AddAsync(_journal);
                    LoadJournalsCommand.Execute(null);
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
                JournalsListView.IsVisible = true;
                listViewNoItems.IsVisible = false;

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

                if (ex.Message.Contains("No journals found for this author."))
                {
                    Journals.Clear();

                    JournalsListView.IsVisible = false;
                    listViewNoItems.IsVisible = true;
                }

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
