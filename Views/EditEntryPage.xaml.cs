using System;
using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class EditEntryPage : ContentPage
    {
		EntryDetailViewModel viewModel;

		public EditEntryPage()
		{
			InitializeComponent();

			var entry = new Entry
			{
				Title = "This is an entry"
			};

			viewModel = new EntryDetailViewModel(entry);
			BindingContext = viewModel;
		}

        public EditEntryPage(EntryDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

		async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var entry = viewModel.Entry;

            var entryVersion = new EntryVersion
            {
                TextEntry = entryText.Text,
                ModifyReason = modifyReason.Text
            };

            entry.EntryVersion = entryVersion;

            MessagingCenter.Send(this, "EditEntry", entry);

            await Navigation.PopAsync();
        }
    }
}
