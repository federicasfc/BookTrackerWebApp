using BookTracker.Server.Services.GenreServices;
using BookTracker.Shared.Models.Genre;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        //Field 

        private readonly IGenreService _genreService;

        //Constructor

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;   

        }

        //Endpoints

        //Gets

        //GetAll
        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenresAsync();

            return Ok(genres);

        }

        //ById

        [HttpGet("{id}")]

        public async Task<IActionResult> Genre(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);

            if(genre is null)
                return NotFound();

            return Ok(genre);
        }

        //GetBooksByGenre

        [HttpGet("booklist/{id}")]

        public async Task<IActionResult> BookList(int id)
        {
            var books = await _genreService.GetBooksByGenreAsync(id);

            if(books is null)
                return NoContent();

            return Ok(books);

        }

        //Create

        [HttpPost("create")]

        public async Task<IActionResult> Create(GenreCreate model)
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (!await _genreService.CreateGenreAsync(model))
                return UnprocessableEntity();

            return Ok("Genre created successfully");
        }

        //Update

        [HttpPut("edit/{id}")]

        public async Task<IActionResult> Edit(int id, GenreUpdate model)
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (id != model.Id)
                return BadRequest();

            if (!await _genreService.UpdateGenreAsync(id, model))
                return BadRequest();

            return Ok("Genre updated successfully");
        }

        //Delete

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var genreToDelete = await _genreService.GetGenreByIdAsync(id);

            if (genreToDelete is null)
                return NotFound();

            if (!await _genreService.DeleteGenreAsync(id))
                return BadRequest();

            return Ok($"Genre {id} deleted successfully");
        }
    }
}
