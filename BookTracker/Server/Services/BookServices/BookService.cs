using BookTracker.Server.Data;
using BookTracker.Server.Models;
using BookTracker.Shared.Models.Book;
using BookTracker.Shared.Models.Genre;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.BookServices

{
    public class BookService : IBookService
    {
        //Field

        private readonly ApplicationDbContext _context;

        //Constructor

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methods

        //Gets
        public async Task<IEnumerable<BookListItem>> GetAllBooksAsync()
        {
            var books = await _context.Books
                .Select(b => new BookListItem()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author

                }).ToListAsync();

            return books;
        }

        public async Task<BookDetail> GetBookByIdAsync(int id)
        {
            var bookEntity = await _context.Books
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity == null)
                return null;

            var detail = new BookDetail()
            {
                Id = bookEntity.Id,
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                Description = bookEntity.Description,
                Genres = bookEntity.Genres.Select(g => new GenreListItem()
                {
                    Id = g.Id,
                    Name = g.Name

                }).ToList()
            };

            return detail;
        }


        //maybe a GetBookByTitle, but not super necessary if you can just click link to book on front-end; might be more relevant, if a search feature exists


        //Create

        public async Task<bool> CreateBookAsync(BookCreate model)
        {
            var bookEntity = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description ?? null,

            };

            _context.Books.Add(bookEntity);

            bookEntity.Genres = model.Genres.Select(g => new Genre()
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

            return await _context.SaveChangesAsync() >= 1;

        }

        //Update

        public async Task<bool> UpdateBookAsync(int id, BookUpdate model)
        {
            if (model is null)
                return false;

            
            if (model.Id != id)
                return false;

            var bookEntity = await _context.Books.Include(b => b.Genres).FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity is null)
                return false;

            bookEntity.Title = model.Title;
            bookEntity.Author = model.Author;
            bookEntity.Description = model.Description;

            //For adding genres that don't exist in bookentity but do exist in model
            foreach (GenreListItem genre in model.Genres)
            {

                if (!bookEntity.Genres.Any(g => g.Id == genre.Id))
                {
                    bookEntity.Genres.Add(new Genre()
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    }
                   );
                }
            }

            //For removing books that exist in BookEntity but don't exist in model(got unchecked):

            //So setting genresToRemove equal to whatever genres that exist in BookEntity that do not have an Id that matches the id of a genre in model.Genres
            //g = Genre entity that exists in BookEntity
            //r = GenreListItem that exists in model.Genres

            var genresToRemove = bookEntity.Genres.Where(g => !model.Genres.Any(r => r.Id == g.Id)).ToList();

            //Then, foreaching through the list genresToRemove and removing the individual genres from bookEntity.Genres
            //extra list genresToRemove is necessary because foreach looping doesn't allow for the collection to be modified while it is being looped through

            foreach(Genre genreToRemove in genresToRemove)
            {
                bookEntity.Genres.Remove(genreToRemove);
            }

                return await _context.SaveChangesAsync() >= 1;


            } //works with genre

            //Delete

            public async Task<bool> DeleteBookAsync(int id)
            {
                var bookToDelete = await _context.Books.FindAsync(id);

                if (bookToDelete is null)
                    return false;

                _context.Books.Remove(bookToDelete);

                return await _context.SaveChangesAsync() == 1;

            }
        }
    }
