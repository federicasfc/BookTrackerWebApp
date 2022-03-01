using BookTracker.Shared.Models.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.BookServices
{
    public interface IBookService
    {

        Task<IEnumerable<BookListItem>> GetAllBooksAsync();

        Task<BookDetail> GetBookByIdAsync(int id);
        Task<bool> CreateBookAsync(BookCreate model);

        Task<bool> UpdateBookAsync(int id, BookUpdate model);

        Task<bool> DeleteBookAsync(int id);
    }
}
