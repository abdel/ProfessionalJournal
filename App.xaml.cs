using System;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class App : Application
    {
        public static string AppName { get { return "sdp.Professional-Journal"; } }

        public static ICredentialsService CredentialsService { get; private set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<JournalDataStore>();
            DependencyService.Register<EntryDataStore>();

            CredentialsService = new CredentialsService();
            if (CredentialsService.DoCredentialsExist())
            {
                MainPage = new NavigationPage(new JournalsPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
