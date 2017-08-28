using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProfessionalJournal
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
        }

		async void OnNavigateRegister(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RegisterPage());
		}

        async void OnNavigateLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
