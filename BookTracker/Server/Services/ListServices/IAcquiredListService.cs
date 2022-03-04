using BookTracker.Shared.Models.List.AcquiredList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public interface IAcquiredListService
    {
        Task<IEnumerable<AcquiredListListItem>> GetAcquiredList();

        Task<AcquiredListDetail> GetAcquiredListItemById(int id);

        Task<bool> CreateAcquiredListItem(AcquiredListCreate model);

        //potentially could work both for editing something on the acquired list and for transferring from reading list to acquired? Might need two separate ones
        Task<bool> UpdateAcquiredList(int id, AcquiredListEdit model);

        Task<bool> DeleteAcquiredListItem(int id);
    }
}
