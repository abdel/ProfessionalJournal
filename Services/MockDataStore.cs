using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfessionalJournal
{
    public class MockDataStore : IDataStore<Journal>
    {
        List<Journal> journals;

        public MockDataStore()
        {
            journals = new List<Journal>();
            var mockJournals = new List<Journal>
            {
                new Journal { Id = Guid.NewGuid().ToString(), Name = "First journal", Description="This is a journal description." },
                new Journal { Id = Guid.NewGuid().ToString(), Name = "Second journal", Description="This is a journal description." },
                new Journal { Id = Guid.NewGuid().ToString(), Name = "Third journal", Description="This is a journal description." },
            };

            foreach (var journal in mockJournals)
            {
                journals.Add(journal);
            }
        }

        public async Task<bool> AddJournalAsync(Journal journal)
        {
            journals.Add(journal);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateJournalAsync(Journal journal)
        {
            var _journal = journals.Where((Journal arg) => arg.Id == journal.Id).FirstOrDefault();
            journals.Remove(_journal);
            journals.Add(journal);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteJournalAsync(string id)
        {
            var _journal = journals.Where((Journal arg) => arg.Id == id).FirstOrDefault();
            journals.Remove(_journal);

            return await Task.FromResult(true);
        }

        public async Task<Journal> GetJournalAsync(string id)
        {
            return await Task.FromResult(journals.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Journal>> GetJournalsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(journals);
        }
    }
}
