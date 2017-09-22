using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class JournalsPage : ContentPage
    {
        JournalsViewModel viewModel;

        public JournalsPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
                         
            BindingContext = viewModel = new JournalsViewModel();
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
            App.CredentialsService.DeleteCredentials();
            Navigation.InsertPageBefore(new LoginPage(), this);
            await Navigation.PopAsync();
        }

		public void OnHide(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			DisplayAlert("Hide Context Action", mi.CommandParameter + " hide context action", "OK");
		}

		public void OnDelete(object sender, EventArgs e)
		{
			var mi = ((MenuItem)sender);
			DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Journals.Count == 0)
                viewModel.LoadJournalsCommand.Execute(null);
        }
    }
}
