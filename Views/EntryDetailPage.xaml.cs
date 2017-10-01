using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class EntryDetailPage : ContentPage
    {
        EntryDetailViewModel viewModel;

        public EntryDetailPage()
        {
            InitializeComponent();

            var entry = new Entry
            {
                Title = "This is an entry"
            };

            NavigationPage.SetBackButtonTitle(this, "");
            viewModel = new EntryDetailViewModel(entry);
            BindingContext = viewModel;
        }

        public EntryDetailPage(EntryDetailViewModel viewModel)
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "");
            BindingContext = this.viewModel = viewModel;
        }

        async void OnEditEntryButtonClicked(object sender, EventArgs e)
        {
            var entry = viewModel.Entry;
            await Navigation.PushAsync(new EditEntryPage(new EntryDetailViewModel(entry)));
        }

    }
}
