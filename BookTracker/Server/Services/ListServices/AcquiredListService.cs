using BookTracker.Server.Data;
using BookTracker.Server.Models;
using BookTracker.Shared.Models.List.AcquiredList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public class AcquiredListService : IAcquiredListService
    {
        //Field 

        private readonly ApplicationDbContext _context;

        private string _userId;

        //Constructor

        public AcquiredListService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methods

        //Gets
        public async Task<IEnumerable<AcquiredListListItem>> GetAcquiredListAsync()
        {
            var acquiredList = await _context.AcquiredLists
                .Where(l => l.UserId == _userId)
                .Select(l => new AcquiredListListItem()
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    Format = l.Format  //(Shared.Models.List.Format) could also have casted this, and kept the one in AcquiredList entity, but redundant

                }).ToListAsync();

            return acquiredList;
        }

        public async Task<AcquiredListDetail> GetAcquiredListItemByIdAsync(int id)
        {
            var acquiredListItem = await _context.AcquiredLists
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == id && l.UserId == _userId);

            if (acquiredListItem == null)
                return null;

            var detail = new AcquiredListDetail()
            {
                Id = acquiredListItem.Id,
                BookTitle = acquiredListItem.Book.Title,
                AddedUtc = acquiredListItem.AddedUtc,
                AcquiredUtc = acquiredListItem.AcquiredUtc,
                Format = acquiredListItem.Format,
                HowAcquired = acquiredListItem.HowAcquired
            };

            return detail;
           
        }

        //Create
        public async Task<bool> CreateAcquiredListItemAsync(AcquiredListCreate model)
        {
            var acquiredListItem = new AcquiredList()
            {
                BookId = model.BookId,
                UserId = _userId,
                AddedUtc = DateTimeOffset.Now,
                AcquiredUtc = model.AcquiredUtc, //will have to do some type of InputDate(?) on the front-end
                Format = model.Format, //again some type of dropdown a la category on front-end InputSelect(?)
                HowAcquired = model.HowAcquired
            };

            _context.AcquiredLists.Add(acquiredListItem);

            return await _context.SaveChangesAsync() == 1;
            
        }

        //Update
        public async Task<bool> UpdateAcquiredListItemAsync(int id, AcquiredListEdit model)
        {
            if (model.Id != id)
                return false;

            var acquiredListItem = await _context.AcquiredLists.FindAsync(id); 
                
            if (acquiredListItem?.UserId != _userId)
                return false;

            acquiredListItem.AcquiredUtc = model.AcquiredUtc;
            acquiredListItem.Format = model.Format;
            acquiredListItem.HowAcquired = model.HowAcquired;

            return await _context.SaveChangesAsync() == 1;


        }

        public async Task<bool> AddToAcquiredListFromReadingAsync(int id, AcquiredListEdit model)
        {
            if (model.Id != id)
                return false;

            var readingListItem = await _context.ReadingLists.FindAsync(id); 
                
            if (readingListItem?.UserId != _userId)
                return false;
            var acquiredListItem = new AcquiredList()
            {
                Id = readingListItem.Id,
                BookId = readingListItem.BookId,
                AddedUtc = readingListItem.AddedUtc,
                UserId = _userId,
                AcquiredUtc = model.AcquiredUtc,
                Format = model.Format,
                HowAcquired = model.HowAcquired

            };

            _context.ReadingLists.Remove(readingListItem);
            _context.AcquiredLists.Add(acquiredListItem);



            return await _context.SaveChangesAsync() == 2;

            //might be more complicated: may have to create a new AcquiredList and then remove the readingListItem from ReadingList
        }
        
        //Delete
        public async Task<bool> DeleteAcquiredListItemAsync(int id)
        {
            var acquiredListItem = await _context.AcquiredLists.FindAsync(id);

            if (acquiredListItem?.UserId != _userId)
                return false;

            _context.AcquiredLists.Remove(acquiredListItem);

            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(string userId) => _userId = userId;

    }

    /* Enum politics:
     * 
     * -Either could have enum declaration in one shared namespace like in Shared (what I have now) or even in a separate Enum-only classlib
     *  -Advantages: only have to declare enum once, no casting, and if changes are made, they only happen in one place (imo, reducing likelihood of human error)
     *  -Disadvantage: makes Data layer dependent on another layer
     * -Or, could keep the data layer fully independent (declare the enum in both data layer and shared)
     *  -Advantage: Data layer stays independent
     *  -Disadvantages: have to cast every time between the two (even though identical) and make changes in both places every time
     * 
     * 
     */
}
