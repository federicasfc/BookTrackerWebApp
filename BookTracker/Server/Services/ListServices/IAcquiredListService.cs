using BookTracker.Shared.Models.List.AcquiredList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public interface IAcquiredListService
    {
        Task<IEnumerable<AcquiredListListItem>> GetAcquiredListAsync();

        Task<AcquiredListDetail> GetAcquiredListItemByIdAsync(int id);

        Task<bool> CreateAcquiredListItemAsync(AcquiredListCreate model);

        //potentially could work both for editing something on the acquired list and for transferring from reading list to acquired? Might need two separate ones
        Task<bool> UpdateAcquiredListItemAsync(int id, AcquiredListEdit model);

        Task<bool> AddToAcquiredListFromReadingAsync(int id, AcquiredListEdit model);

        Task<bool> DeleteAcquiredListItemAsync(int id);

        void SetUserId(string userId);
    }
}
