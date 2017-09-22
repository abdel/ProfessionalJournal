using System;
using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class NewJournalPage : ContentPage
    {
        public NewJournalPage()
        {
            InitializeComponent();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var journal = new Journal
            {
                Title = newJournalTitle.Text,
                Description = newJournalDescription.Text
            };

            MessagingCenter.Send(this, "AddJournal", journal);
            await Navigation.PopAsync();
        }
    }
}
