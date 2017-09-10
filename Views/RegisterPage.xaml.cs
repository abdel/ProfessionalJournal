using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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

            this.client = new MobileServiceClient(Constants.BackendURL);
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
                var result = await client.InvokeApiAsync<Author, ResponseSuccess>("register", author);

                await DisplayAlert("Success", "You have been registered as an author.", "OK");
            }
            catch (Exception e)
            {
                this.errored = true;
                await DisplayAlert("Error", e.Message, "OK");
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

                    FirstName = newAuthorFirstName.Text,
                    LastName = newAuthorLastName.Text,
                    Email = newAuthorEmail.Text,
                    DateOfBirth = newAuthorDateOfBirth.Date,
                    Username = newAuthorUsername.Text.ToLower(),
                    Password = AppHelper.GeneratePasswordHash(
                        newAuthorPassword.Text
                    )
                };

                // Register author
                await RegisterAuthor(author);


                // Hide loading indicator
                UserDialogs.Instance.HideLoading();
               
                // Go back to login page on success
                if (this.errored != true) {
                    await Navigation.PopAsync();
                }
            }
        }
    }
}
