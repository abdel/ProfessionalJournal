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

            if (journal.Title != null && journal.Description != null)
			{
				MessagingCenter.Send(this, "AddJournal", journal);
				await Navigation.PopAsync();
			}
			else
			{
				if (journal.Title == null)
				{
					await DisplayAlert("Error", "The journal title can't be empty!", "Try again");
				}
                else if (journal.Description == null)
				{
					await DisplayAlert("Error", "The journal description can't be empty!", "Try again");
				}
			}
        }
    }
}
