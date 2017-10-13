using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class EntryHistoryPage : ContentPage
    {
        EntryHistoryViewModel viewModel;

        public EntryHistoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new EntryHistoryViewModel("1");
        }

        public EntryHistoryPage(EntryHistoryViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        async void OnEntrySelected(object sender, SelectedItemChangedEventArgs args)
        {
            var entry = args.SelectedItem as Entry;
            if (entry == null)
                return;

            await Navigation.PushAsync(new EntryDetailPage(new EntryDetailViewModel(entry, false)));

            // Manually deselect journal
            EntryHistoryListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadEntryHistoryCommand.Execute(null);
        }
    }
}
