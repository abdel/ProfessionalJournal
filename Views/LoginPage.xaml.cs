using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProfessionalJournal.Models;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Acr.UserDialogs;

namespace ProfessionalJournal
{
    public partial class RegisterPage : ContentPage
    {
        Boolean errored;
        MobileServiceClient client;

        /// <summary>
        /// Constructor for RegisterPage class that sets up the MobileServiceClient
        /// </summary>
        public RegisterPage()
        {
            InitializeComponent();

            this.client = new MobileServiceClient(Constants.ApplicationURL);
        }

        /// <summary>
        /// Registers a new author by sending a request to the /api/register
        /// endpoint
        /// </summary>
        /// <param name="author">An Author model object</param>
        async Task RegisterAuthor(Author author)
        {
            try
            {
                // Send request to POST /api/register endpoint
                var result = await client.InvokeApiAsync<Author, string>("register", author);

                await DisplayAlert("Success", "You have been registered as an author.", "OK");
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
                this.errored = true;
            }
        }


        /// <summary>
        /// Triggered by clicking the register button on RegisterPage to
        /// create new authors
        /// </summary>
        public async void OnRegister(object sender, EventArgs e)
        {
            if (newAuthorPassword.Text != newAuthorConfirmPassword.Text) {
                await DisplayAlert("Error", "Your passwords don't match!", "Try again");
            } else {
                // Show loading indicator
                UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);

                // New author object
                var author = new Author
                {
                    Username = newAuthorUsername.Text.ToLower(),
                    Password = AppHelper.GeneratePasswordHash(
                        newAuthorPassword.Text
                    )
                };

                // Register author
                await RegisterAuthor(author);


                // Hide loading indicator
                UserDialogs.Instance.HideLoading();

                // Clear the register form
                newAuthorUsername.Text = string.Empty;
                newAuthorPassword.Text = string.Empty;
                newAuthorConfirmPassword.Text = string.Empty;
                newAuthorUsername.Unfocus();
                newAuthorPassword.Unfocus();
                newAuthorConfirmPassword.Unfocus();
            }
        }
    }
}
