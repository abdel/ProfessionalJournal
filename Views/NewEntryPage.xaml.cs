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
                Title = newEntryTitle.Text
            };

            var entryVersion = new EntryVersion
            {
                VersionTrackId = 1,
                TextEntry = newEntryText.Text
            };

            entry.EntryVersion = entryVersion;

            MessagingCenter.Send(this, "AddEntry", entry);
            await Navigation.PopAsync();
        }
    }
}
