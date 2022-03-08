using BookTracker.Server.Data;
using BookTracker.Server.Models;
using BookTracker.Shared.Models.List.ReadList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public class ReadListService : IReadListService
    {

        //Field

        private readonly ApplicationDbContext _context;

        private string _userId;

        //Constructor

        public ReadListService(ApplicationDbContext context)
        {
            _context = context;

        }

        //Methods

        //Gets
        public async Task<IEnumerable<ReadListListItem>> GetReadListAsync()
        {
            var readList = await _context.ReadLists
                .Where(l => l.UserId == _userId)
                .Select(l => new ReadListListItem()
                {
                    Id = l.Id,
                    BookTitle = l.Book.Title,
                    IsFinished = l.IsFinished
                }).ToListAsync();

            return readList;
        }

        public async Task<ReadListDetail> GetReadListItemByIdAsync(int id)
        {
            var readListItem = await _context.ReadLists
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == id && l.UserId == _userId);

            if (readListItem == null)
                return null;

            var detail = new ReadListDetail()
            {
                Id = readListItem.Id,
                BookId = readListItem.BookId,
                BookTitle = readListItem.Book.Title,
                StartedUtc = readListItem.StartedUtc,
                AddedUtc = readListItem.AddedUtc,
                AcquiredUtc = readListItem.AcquiredUtc,
                Format = readListItem.Format,
                HowAcquired = readListItem.HowAcquired,
                IsFinished = readListItem.IsFinished,
                Review = readListItem.Review
            };

            return detail;
        }

        //Create
        public async Task<bool> CreateReadListItemAsync(ReadListCreate model)
        {
            var readListItem = new ReadList()
            {
                BookId = model.BookId,
                UserId = _userId,
                AddedUtc = DateTimeOffset.Now,
                AcquiredUtc = model.AcquiredUtc, //will have to do some type of InputDate(?) on the front-end
                StartedUtc = model.StartedUtc,
                Format = model.Format, //again some type of dropdown a la category on front-end InputSelect(?)
                HowAcquired = model.HowAcquired,
                IsFinished = model.IsFinished,
                Review = model.Review

            };

            _context.ReadLists.Add(readListItem);

            return await _context.SaveChangesAsync() == 1;
        }


        //Update
        public async Task<bool> UpdateReadListItemAsync(int id, ReadListEdit model)
        {

            if (model.Id != id)
                return false;

           var readListItem = await _context.ReadLists.FindAsync(id);
            

            if (readListItem?.UserId != _userId)
                return false;

            readListItem.AcquiredUtc = model.AcquiredUtc;
            readListItem.Format = model.Format;
            readListItem.HowAcquired = model.HowAcquired;
            readListItem.StartedUtc = model.StartedUtc;
            readListItem.IsFinished = model.IsFinished;
            readListItem.Review = model.Review;

            return await _context.SaveChangesAsync() == 1;
        }

        //AddToReadFromReading

        public async Task<bool> AddToReadListFromReadingAsync(int id, ReadListEdit model)
        {
            if (model.Id != id)
                return false;

            var readingListItem = await _context.ReadingLists.FindAsync(id);

            if (readingListItem?.UserId != _userId)
                return false;
            var readListItem = new ReadList()
            {
                Id = readingListItem.Id,
                BookId = readingListItem.BookId,
                AddedUtc = readingListItem.AddedUtc,
                UserId = _userId,
                AcquiredUtc = model.AcquiredUtc,
                Format = model.Format,
                HowAcquired = model.HowAcquired,
                StartedUtc = model.StartedUtc,
                IsFinished = model.IsFinished,
                Review = model.Review

            };

            _context.ReadingLists.Remove(readingListItem);
            _context.ReadLists.Add(readListItem);



            return await _context.SaveChangesAsync() >= 1;

            
        }

        //AddToReadFromAcquiredList
        public async Task<bool> AddToReadListFromAcquiredAsync(int id, ReadListFromAcquiredEdit model)
        {
            if (model.Id != id)
                return false;

            var acquiredListItem = await _context.AcquiredLists.FindAsync(id);

            if (acquiredListItem?.UserId != _userId)
                return false;
            var readListItem = new ReadList()
            {
                Id = acquiredListItem.Id,
                BookId = acquiredListItem.BookId,
                AddedUtc = acquiredListItem.AddedUtc,
                UserId = _userId,
                AcquiredUtc = acquiredListItem.AcquiredUtc,
                Format = acquiredListItem.Format,
                HowAcquired = acquiredListItem.HowAcquired,
                StartedUtc = model.StartedUtc,
                IsFinished = model.IsFinished,
                Review = model.Review


            };

            _context.AcquiredLists.Remove(acquiredListItem);
            _context.ReadLists.Add(readListItem);



            return await _context.SaveChangesAsync() >= 1;

        }

        

        //Delete
        public async Task<bool> DeleteReadListItemAsync(int id)
        {
            var readListItem = await _context.ReadLists.FindAsync(id);


            if (readListItem?.UserId != _userId)
                return false;

            _context.ReadLists.Remove(readListItem);

            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(string userId) => _userId = userId;
    }
}
