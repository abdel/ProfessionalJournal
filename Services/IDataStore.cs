using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfessionalJournal
{
    public interface IDataStore<T>
    {
        Task<bool> AddAsync(T instance);
        Task<bool> UpdateAsync(T instance);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false);
    }
}
