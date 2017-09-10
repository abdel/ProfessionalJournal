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

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddJournal", Journal);
            await Navigation.PopToRootAsync();
        }
    }
}
