using BookTracker.Shared.Models.List.ReadingList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public interface IReadingListService
    {
        Task<IEnumerable<ReadingListListItem>> GetReadingListAsync();

        Task<ReadingListDetail> GetReadingListItemByIdAsync(int id);

        Task<bool> CreateReadingListItemAsync(ReadingListCreate model);

        Task<bool> DeleteReadingListItemAsync(int id);

        void SetUserId(string userId);







        //GetAcquiredList
        //CreateAcquiredList (AcquiredListCreate model)
        //EditAcquiredList (AcquiredListEdit model)
        //MoveBookFromReadingListToAcquiredList (some type of update method, would have to take in an id of the existing list and add extra props?)

    }
}
