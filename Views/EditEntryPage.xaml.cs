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
                EntryId = entry.Id,
                TextEntry = entryText.Text,
                ModifyReason = modifyReason.Text,
                VersionTrackId = entry.EntryVersion.VersionTrackId + 1
            };

            entry.EntryVersion = entryVersion;

			if (entryVersion.ModifyReason != null && entryVersion.TextEntry != null)
			{
				MessagingCenter.Send(this, "EditEntry", entry);
				await Navigation.PopAsync();
			}
			else
			{
				if (entryVersion.ModifyReason == null)
				{
					await DisplayAlert("Error", "The modification reason can't be empty!", "Try again");
				}
				else if (entryVersion.TextEntry == null)
				{
					await DisplayAlert("Error", "The entry text can't be empty!", "Try again");
				}
			}
        }
    }
}
