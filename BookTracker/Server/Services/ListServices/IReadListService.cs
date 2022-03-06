using BookTracker.Shared.Models.List.ReadList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public interface IReadListService
    {
        Task<IEnumerable<ReadListListItem>> GetReadListAsync();

        Task<ReadListDetail> GetReadListItemByIdAsync(int id);

        Task<bool> CreateReadListItemAsync(ReadListCreate model);

        Task<bool> UpdateReadListItemAsync(int id, ReadListEdit model);

        Task<bool> AddToReadListFromAcquiredAsync(int id, ReadListFromAcquiredEdit model);

        Task<bool> DeleteReadListItemAsync(int id);

        void SetUserId(string userId);
    }
}
