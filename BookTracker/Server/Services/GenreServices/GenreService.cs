using BookTracker.Server.Data;
using BookTracker.Server.Models;
using BookTracker.Shared.Models.Book;
using BookTracker.Shared.Models.Genre;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.GenreServices

{
    public class GenreService : IGenreService
    {
        //Field

        private readonly ApplicationDbContext _context;

        //Constructor

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Methods

        //Gets

        public async Task<IEnumerable<GenreListItem>> GetAllGenresAsync()
        {
            var genres = await _context.Genres
                .Select(g => new GenreListItem()
                {
                    Id = g.Id,
                    Name = g.Name

                }).ToListAsync();

            return genres;
        }

        public async Task<GenreDetail> GetGenreByIdAsync(int id)
        {
            var genreEntity = await _context.Genres.FindAsync(id);

            if (genreEntity is null)
                return null;

            var detail = new GenreDetail()
            {
                Id = genreEntity.Id,
                Name = genreEntity.Name

            };

            return detail;
            
        }

        //GetBooksByGenre
         /*
     public async Task<GenreBooksListItem> GetBooksByGenreAsync(int id) //genreId? maybe better?
        {
            var genreEntity = await _context.Genres
               .Include(g => g.Books)
               .FirstOrDefaultAsync(e => e.Id == id);

            if(genreEntity is null)
                return null;
            
            var booksByGenre = new GenreBooksListItem()
                {
                Books = genreEntity.Books.Select(e => new BookListItem()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Author = e.Author

                }).ToList()
                };
                 
                return booksByGenre;
        } */

        /*
        public async Task<GenreBooksListItem> GetBooksByGenreAsync(int id)
        {
            var genreEntity = await _context.Genres
                .Include(g => g.Books)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (genreEntity is null)
                return null;

            var booksByGenre = new GenreBooksListItem()
            {
                Books = genreEntity.Books.Where(b )
            }
        }
        */


        //Create

        public async Task<bool> CreateGenreAsync(GenreCreate model)
        {
            if (model is null)
                return false;

            var genreEntity = new Genre()
            {
                Name = model.Name,

            };

            _context.Genres.Add(genreEntity);

            return await _context.SaveChangesAsync() == 1;
            
        }

        //Update
        public async Task<bool> UpdateGenreAsync(int id, GenreUpdate model)
        {
            if (model is null)
                return false;

            if (model.Id != id)
                return false;

            var genreEntity = await _context.Genres.FindAsync(id);

            if (genreEntity is null)
                return false;

            genreEntity.Name = model.Name;

            return await _context.SaveChangesAsync() == 1;

            
        }

        //Delete

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genreToDelete = await _context.Genres.FindAsync(id);

            if (genreToDelete is null)
                return false;

            _context.Genres.Remove(genreToDelete);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
