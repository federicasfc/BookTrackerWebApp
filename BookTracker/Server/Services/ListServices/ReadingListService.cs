using BookTracker.Server.Data;
using BookTracker.Server.Models;
using BookTracker.Shared.Models.List.ReadingList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.ListServices
{
    public class ReadingListService : IReadingListService
    {
        //Field 

        private string _userId;

        private readonly ApplicationDbContext _context;


        //Constructor

        public ReadingListService(ApplicationDbContext context)
        {
            _context = context;

        }

        //Methods

        //Gets

        public async Task<IEnumerable<ReadingListListItem>> GetReadingListAsync()
        {
            var readingList = await _context.ReadingLists
                .Where(l => l.UserId == _userId)
                .Select(l => new ReadingListListItem()
            {
                    Id = l.Id,
                    BookTitle = l.Book.Title

            }).ToListAsync();

            return readingList;
            
        }

        public async Task<ReadingListDetail> GetReadingListItemByIdAsync(int id)
        {
            var readingListItem = await _context.ReadingLists
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == id && l.UserId == _userId);


            if (readingListItem == null)
                return null;

            var detail = new ReadingListDetail()
            {
                Id = readingListItem.Id,
                BookTitle = readingListItem.Book.Title,
                BookId = readingListItem.Book.Id,
                AddedUtc = readingListItem.AddedUtc

            };

            return detail;
        }

        //Create
        public async Task<bool> CreateReadingListItemAsync(ReadingListCreate model)
        {
            
            var readingListItem = new ReadingList()
            {
                BookId = model.BookId,
                UserId = _userId,
                AddedUtc = DateTimeOffset.Now
            };

            _context.ReadingLists.Add(readingListItem);

            return await _context.SaveChangesAsync() == 1;

        }

        //Delete

        public async Task<bool> DeleteReadingListItemAsync(int id)
        {
            var readingListItem = await _context.ReadingLists.FindAsync(id);

            if(readingListItem?.UserId != _userId)
                return false;

            _context.ReadingLists.Remove(readingListItem);

            return await _context.SaveChangesAsync() == 1;
            
        } 
        public void SetUserId(string userId) => _userId = userId;

    }
}
