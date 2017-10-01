using System;
using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class NewEntryPage : ContentPage
    {
        public NewEntryPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var entry = new Entry
            {
                Title = newEntryTitle.Text,
                Location = newEntryLocation.Text
            };

            var entryVersion = new EntryVersion
            {
                VersionTrackId = 1,
                TextEntry = newEntryText.Text
            };

            entry.EntryVersion = entryVersion;

            if (entry.Title != null && entry.Location != null && entryVersion.TextEntry != null) {
                MessagingCenter.Send(this, "AddEntry", entry);
                await Navigation.PopAsync();
            } else {
                if (entry.Title == null) {
                    await DisplayAlert("Error", "The entry title can't be empty!", "Try again");
                } else if (entry.Location == null) {
                    await DisplayAlert("Error", "The current location can't be empty!", "Try again");
                } else if (entryVersion.TextEntry == null) {
                    await DisplayAlert("Error", "The entry text can't be empty!", "Try again");
                }
            }
        }
    }
}
