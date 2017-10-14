using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProfessionalJournal
{
	public partial class JournalsPage : ContentPage
    {
        JournalsViewModel viewModel;

        public JournalsPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");
            NavigationPage.SetHasBackButton(this, false);

            BindingContext = viewModel = new JournalsViewModel();

            MessagingCenter.Subscribe<JournalsViewModel>(this, "AuthorLogout", async (obj) =>
            {
                await DoLogout();
            });
        }

        async void OnJournalSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var journal = args.SelectedItem as Journal;
            if (journal == null)
                return;

            await Navigation.PushAsync(new JournalEntriesPage(new JournalEntriesViewModel(journal)));

            // Manually deselect journal
            JournalsListView.SelectedItem = null;
        }

        async void OnAddJournalButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewJournalPage());
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
			MessagingCenter.Send(this, "AuthorLogout");
            await DoLogout();
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }

        async Task DoLogout()
        {
            App.CredentialsService.DeleteCredentials();

            IReadOnlyList<Page> pageStack = Application.Current.MainPage.Navigation.NavigationStack;

            if (pageStack.Count >= 1 && pageStack[0].ToString() != "ProfessionalJournal.LoginPage")
            {
                Navigation.InsertPageBefore(new LoginPage(), this);
            }

            viewModel.UnsubscribeFromMessages();

            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.viewModel.listViewNoItems = listViewNoItems;
            this.viewModel.JournalsListView = JournalsListView;

            viewModel.LoadJournalsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
