using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace ProfessionalJournal
{
    public class EntryDataStore : IDataStore<Entry>
    {
        string journalId;
        HttpClient client;
        IEnumerable<Entry> entries;

		public EntryDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{Constants.BackendURL}/");

			entries = new List<Entry>();
		}

        public EntryDataStore(string currentJournalId)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{Constants.BackendURL}/");

            entries = new List<Entry>();
            journalId = currentJournalId;
        }

        public async Task<IEnumerable<Entry>> GetAllAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/entry?journal_id={journalId}");
                entries = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Entry>>(json));
            }

            return entries;
        }

        public async Task<Entry> GetAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/entry/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Entry>(json));
            }

            return null;
        }

        public async Task<bool> AddAsync(Entry entry)
        {
            if (entry == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedEntry = JsonConvert.SerializeObject(entry);

            var response = await client.PostAsync($"api/entry", new StringContent(serializedEntry, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Entry entry)
        {
            if (entry == null || entry.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedEntry = JsonConvert.SerializeObject(entry);
            var buffer = Encoding.UTF8.GetBytes(serializedEntry);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/entry/{entry.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/entry/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
