using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfessionalJournal
{
    public interface IDataStore<T>
    {
        Task<bool> AddJournalAsync(T journal);
        Task<bool> UpdateJournalAsync(T journal);
        Task<bool> DeleteJournalAsync(string id);
        Task<T> GetJournalAsync(string id);
        Task<IEnumerable<T>> GetJournalsAsync(bool forceRefresh = false);
    }
}
