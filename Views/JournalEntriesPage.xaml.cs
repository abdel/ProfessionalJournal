using System;
using Xamarin.Forms;

using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;

namespace ProfessionalJournal
{
    public partial class JournalEntriesPage : ContentPage
    {
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        JournalEntriesViewModel viewModel;

        public JournalEntriesPage()
        {
            InitializeComponent();

            var journal = new Journal
            {
                Title = "Journal 1",
                Description = "This is a journal description."
            };

            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = new JournalEntriesViewModel(journal);
            BindingContext = viewModel;
        }

        public JournalEntriesPage(JournalEntriesViewModel viewModel)
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");
            BindingContext = this.viewModel = viewModel;
            this.viewModel.searchBar = searchBar;
        }

        async void OnEntrySelected(object sender, SelectedItemChangedEventArgs args)
        {
            var entry = args.SelectedItem as Entry;
            if (entry == null)
                return;

            await Navigation.PushAsync(new EntryPage(entry));

            // Manually deselect entry
            JournalEntriesListView.SelectedItem = null;
        }

        async void OnAddEntryButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEntryPage());
        }

        public void OnHide(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var entry = mi.CommandParameter as Entry;

            MessagingCenter.Send(this, "HideEntry", entry);
        }

        public void OnUnhide(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var entry = mi.CommandParameter as Entry;

            MessagingCenter.Send(this, "UnhideEntry", entry);
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var entry = mi.CommandParameter as Entry;

            MessagingCenter.Send(this, "DeleteEntry", entry);
        }

		public void ToggleAllEntries(object sender, ToggledEventArgs e)
		{
            viewModel.toggleAll = e.Value;
			viewModel.LoadEntriesCommand.Execute(null);
		}

        public async void OnDateSearch(object sender, EventArgs e)
        {
            var page = new SearchPopupPage();
            await Navigation.PushPopupAsync(page);
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            // Has Cancel has been pressed?
            if (textChangedEventArgs.NewTextValue == "" || textChangedEventArgs.NewTextValue == null)
            {
                viewModel.LoadEntriesCommand.Execute(null);   
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.viewModel.listViewNoItems = listViewNoItems;
            this.viewModel.EntriesListView = JournalEntriesListView;

            viewModel.LoadEntriesCommand.Execute(null);
        }
    }
}
