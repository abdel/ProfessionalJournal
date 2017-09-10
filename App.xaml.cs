using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<JournalDataStore>();
            DependencyService.Register<EntryDataStore>();

            MainPage = new NavigationPage(new MainPage());
        }
    }
}
