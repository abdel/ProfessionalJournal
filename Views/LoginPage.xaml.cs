using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProfessionalJournal.Models;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Acr.UserDialogs;

namespace ProfessionalJournal
{
    public partial class LoginPage : ContentPage
    {
        Boolean errored;
        MobileServiceClient client;

        /// <summary>
        /// Constructor for Loginpage class that sets up the MobileServiceClient
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();

            this.client = new MobileServiceClient(Constants.ApplicationURL);
        }

        /// <summary>
        /// Authenticates the author by sending a request to the /api/login
        /// endpoint
        /// </summary>
        /// <param name="author">An Author model object</param>
        async Task AuthenticateAuthor(Author author)
        {
            try
            {
                // Send request to POST /api/login endpoint
                var result = await client.InvokeApiAsync<Author, ResponseSuccess>("login", author);
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
                this.errored = true;
            }
        }


        /// <summary>
        /// Triggered by clicking the login button on LoginPage to
        /// authenticate the author.
        /// </summary>
        public async void OnLogin(object sender, EventArgs e)
        {
            // Show loading indicator
            UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);

            // Author object
            var author = new Author
            {
                Username = authorUsername.Text.ToLower(),
                Password = AppHelper.GeneratePasswordHash(
                    authorPassword.Text
                )
            };

            // Authenticate author
            await AuthenticateAuthor(author);

            // Hide loading indicator
            UserDialogs.Instance.HideLoading();

            // Clear the login form
            authorUsername.Text = string.Empty;
            authorPassword.Text = string.Empty;
            authorUsername.Unfocus();
            authorPassword.Unfocus();

            // Redirect to journals page
            if (!this.errored)
            {
                await Navigation.PushModalAsync(new AppPage());
            }
        }
    }

    public class ResponseSuccess {
        public string msg { get; set; }
    }
}
