using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.WindowsAzure.MobileServices;

namespace ProfessionalJournal
{
    public interface IDataStore<T>
    {
        Task<bool> AddAsync(T instance);
        Task<bool> UpdateAsync(T instance);
        Task<bool> DeleteAsync(string id);
		Task<bool> HideAsync(string id);
		Task<bool> UnhideAsync(string id);
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(bool forceRefresh = false, string text = null, bool deleted = false, bool hidden = false, DateTime[] dates = null);
        Task<IEnumerable<T>> GetHistoryAsync(string id, bool forceRefresh = false);
        void ResetClient();
    }
}
