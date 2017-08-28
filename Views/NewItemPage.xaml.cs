using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class NewJournalPage : ContentPage
    {
        public Journal Journal { get; set; }

        public NewJournalPage()
        {
            InitializeComponent();

            Journal = new Journal
            {
                Name = "Journal name",
                Description = "This is the journal description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddJournal", Journal);
            await Navigation.PopToRootAsync();
        }
    }
}
