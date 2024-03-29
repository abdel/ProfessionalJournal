﻿using System;
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
            // Reset error
            this.errored = false;

            try
            {
                // Send request to POST /api/author/register endpoint
                var result = await client.InvokeApiAsync<Author, Response>("register", author);

                await DisplayAlert("Success", "You have been registered as an author.", "OK");
            }
            catch (Exception e)
            {
                Console.WriteLine("Login:" + e);

                if (!e.Message.Contains("A task was canceled"))
                {
                    await DisplayAlert("Error", e.Message, "OK");
                    this.errored = true;
                }
            }
        }


        /// <summary>
        /// Triggered by clicking the register button on RegisterPage to
        /// create new authors
        /// </summary>
        public async void OnRegister(object sender, EventArgs e)
        {
            if (newAuthorFirstName.Text != null &&
				newAuthorLastName.Text != null &&
				newAuthorEmail.Text != null &&
                newAuthorDateOfBirth.Date != null &&
				newAuthorUsername.Text != null && 
                newAuthorPassword.Text != null && 
                newAuthorPassword.Text == newAuthorConfirmPassword.Text)
            {
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
                    await Navigation.PopToRootAsync();
                }
            }
            else 
            {
                if (newAuthorUsername.Text == null) 
                {
                    await DisplayAlert("Error", "Your username can't be empty!", "Try again");
                }
                else if (newAuthorFirstName.Text == null)
                {
                    await DisplayAlert("Error", "Your first name can't be empty!", "Try again");
                }
                else if (newAuthorLastName.Text == null)
                {
                    await DisplayAlert("Error", "Your last name can't be empty!", "Try again");
                }
                else if (newAuthorEmail.Text == null)
                {
                    await DisplayAlert("Error", "Your email address can't be empty!", "Try again");
                }
                else if (newAuthorDateOfBirth.Date == null)
                {
                    await DisplayAlert("Error", "Your date of birth can't be empty!", "Try again");
                }
                else if (newAuthorPassword.Text == null)
                {
                    await DisplayAlert("Error", "Your password can't be empty!", "Try again");
                }
                else if (newAuthorPassword.Text != newAuthorConfirmPassword.Text)
                {
                    await DisplayAlert("Error", "Your passwords don't match!", "Try again");
                }
            }
        }
    }
}
