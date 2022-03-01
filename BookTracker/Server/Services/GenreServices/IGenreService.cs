using BookTracker.Shared.Models.Genre;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookTracker.Server.Services.GenreServices
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreListItem>> GetAllGenresAsync();

        Task<GenreDetail> GetGenreByIdAsync(int id);

        Task<GenreBooksListItem> GetBooksByGenreAsync(int id);

        Task<bool> CreateGenreAsync(GenreCreate model);

        Task<bool> UpdateGenreAsync(int id, GenreUpdate model);

        Task<bool> DeleteGenreAsync(int id);
    }
}
