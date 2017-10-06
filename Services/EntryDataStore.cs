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
    public class EntryDataStore : IDataStore<Entry>
    {
        string journalId;
        IEnumerable<Entry> entries;
        readonly MobileServiceClient client;

		public EntryDataStore()
		{
			client = new MobileServiceClient(Constants.BackendURL);
            client.CurrentUser = new MobileServiceUser(App.CredentialsService.Username);
            client.CurrentUser.MobileServiceAuthenticationToken = App.CredentialsService.Token;

			entries = new List<Entry>();
		}

        public EntryDataStore(string currentJournalId)
        {
            client = new MobileServiceClient(Constants.BackendURL);
            client.CurrentUser = new MobileServiceUser(App.CredentialsService.Username);
            client.CurrentUser.MobileServiceAuthenticationToken = App.CredentialsService.Token;

            entries = new List<Entry>();
            journalId = currentJournalId;
        }

        public async Task<IEnumerable<Entry>> GetAllAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.InvokeApiAsync<Response>($"entries?journal_id={journalId}", HttpMethod.Get, null);
                entries = json.entries;
            }

            return entries;
        }

		public async Task<IEnumerable<Entry>> GetHistoryAsync(string id, bool forceRefresh = false)
		{
			if (forceRefresh && CrossConnectivity.Current.IsConnected)
			{
				var json = await client.InvokeApiAsync<Response>($"history?entry_id={id}", HttpMethod.Get, null);
				entries = json.entries;
			}

			return entries;
		}

        public async Task<Entry> GetAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                return await client.InvokeApiAsync<Entry>($"entry?entry_id={id}", HttpMethod.Get, null);;
            }

            return null;
        }

        public async Task<bool> AddAsync(Entry entry)
        {
            if (entry == null || !CrossConnectivity.Current.IsConnected)
                return false;

            entry.JournalId = journalId;
            var response = await client.InvokeApiAsync<Entry, Response>("entry", entry);

            return (response.StatusCode == 200);
        }

        public async Task<bool> UpdateAsync(Entry entry)
        {
            Console.WriteLine(entry.Id);
            if (entry == null || entry.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.InvokeApiAsync<Entry, Response>($"entry?entry_id={entry.Id}", entry, HttpMethod.Put, null);

            return (response.StatusCode == 200);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.InvokeApiAsync<Response>($"entry?entry_id={id}", HttpMethod.Delete, null);

            return (response.StatusCode == 200);
        }
    }
}
