using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProfessionalJournal.Models;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;


namespace ProfessionalJournal
{
    public partial class RegisterPage : ContentPage
    {
        MobileServiceClient client;

        public RegisterPage()
        {
            InitializeComponent();

            this.client = new MobileServiceClient(Constants.ApplicationURL);
        }

        // Data methods
        async Task RegisterAuthor(Author author)
        {
            try
            {
                var result = await client.InvokeApiAsync<Author, AuthorResponse>("register", author);

                await DisplayAlert("Success", "You have been registered as an author.", "OK");
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "OK");
            }
           
        }

        public async void OnRegister(object sender, EventArgs e)
        {
            var author = new Author { 
                Username = newAuthorUsername.Text,
                Password = AppHelper.GeneratePasswordHash(
                    newAuthorPassword.Text
                )
            };

            await RegisterAuthor(author);

            newAuthorUsername.Text = string.Empty;
            newAuthorPassword.Text = string.Empty;
            newAuthorUsername.Unfocus();
            newAuthorPassword.Unfocus();
        }
    }

    public class AuthorResponse
    {
        public string Id { get; set; }
    }


}
