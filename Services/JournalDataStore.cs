using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;
using Microsoft.WindowsAzure.MobileServices;

namespace ProfessionalJournal
{
    public class JournalDataStore : IDataStore<Journal>
    {
        readonly MobileServiceClient client;
        IEnumerable<Journal> journals;

        public JournalDataStore()
        {
            client = new MobileServiceClient(Constants.BackendURL);
            client.CurrentUser = new MobileServiceUser(App.CredentialsService.Username);
            client.CurrentUser.MobileServiceAuthenticationToken = App.CredentialsService.Token;

            journals = new List<Journal>();
        }

        public async Task<IEnumerable<Journal>> GetAllAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.InvokeApiAsync<Response>("journal", HttpMethod.Get, null);
                journals = json.journals;
            }

            return journals;
        }

        public async Task<Journal> GetAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                return await client.InvokeApiAsync<Journal>($"journal/{id}", HttpMethod.Get, null);
            }

            return null;
        }

        public async Task<bool> AddAsync(Journal journal)
        {
            if (journal == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.InvokeApiAsync<Journal, Response>("journal", journal);

            return (response.StatusCode == 200);
        }

        public async Task<bool> UpdateAsync(Journal journal)
        {
            if (journal == null || journal.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.InvokeApiAsync<Journal, Response>($"journal/{journal.Id}", journal, HttpMethod.Put, null);

            return (response.StatusCode == 200);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.InvokeApiAsync<Response>($"journal/{id}", HttpMethod.Delete, null);

            return (response.StatusCode == 200);
        }
    }
}
