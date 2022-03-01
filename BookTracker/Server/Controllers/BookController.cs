using BookTracker.Server.Services.BookServices;
using BookTracker.Shared.Models.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //Field

        private readonly IBookService _bookService;

        //Constructor

        public BookController(IBookService bookService)
        {
            _bookService = bookService;

        }

        //Endpoints

        //Gets

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Book(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        //Create

        [HttpPost]

        public async Task<IActionResult> Create(BookCreate model)
        {
            if (!ModelState.IsValid || model == null)
                return BadRequest(ModelState);

            if (!await _bookService.CreateBookAsync(model))
                return UnprocessableEntity();

            return Ok("Book created successfully");

        }

        //Update

        [HttpPut("edit/{id}")]

        public async Task<IActionResult> Edit(int id, BookUpdate model)
        {
            if (!ModelState.IsValid || model == null)
                return BadRequest(ModelState);

            if (model.Id != id)
                return BadRequest();

            if(!await _bookService.UpdateBookAsync(id, model))
                return BadRequest();

            return Ok("Book updated successfully");


        }

        //Delete

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var bookToDelete = await _bookService.GetBookByIdAsync(id);

            if (bookToDelete is null)
                return NotFound();

            if (!await _bookService.DeleteBookAsync(id))
                return BadRequest();

            return Ok($"Book {id} deleted successfully");
        }





    }
}
