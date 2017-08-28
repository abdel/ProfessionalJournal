using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace ProfessionalJournal
{
    public class CloudDataStore : IDataStore<Journal>
    {
        HttpClient client;
        IEnumerable<Journal> journals;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            journals = new List<Journal>();
        }

        public async Task<IEnumerable<Journal>> GetJournalsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/journal");
                journals = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Journal>>(json));
            }

            return journals;
        }

        public async Task<Journal> GetJournalAsync(string id)
        {
            if (id != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/journal/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Journal>(json));
            }

            return null;
        }

        public async Task<bool> AddJournalAsync(Journal journal)
        {
            if (journal == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedJournal = JsonConvert.SerializeObject(journal);

            var response = await client.PostAsync($"api/journal", new StringContent(serializedJournal, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateJournalAsync(Journal journal)
        {
            if (journal == null || journal.Id == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedJournal = JsonConvert.SerializeObject(journal);
            var buffer = Encoding.UTF8.GetBytes(serializedJournal);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/journal/{journal.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteJournalAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/journal/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
