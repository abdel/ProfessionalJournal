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
        }

		async void OnNavigateRegister(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RegisterPage());
		}
    }
}
