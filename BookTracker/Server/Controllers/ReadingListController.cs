using BookTracker.Server.Services.ListServices;
using BookTracker.Shared.Models.List.ReadingList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingListController : ControllerBase
    {

        //Field

        private readonly IReadingListService _readingListService;

        //Constructor

        public ReadingListController(IReadingListService readingListService)
        {
            _readingListService = readingListService;
        }

        //Methods

        //Gets

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var readingList = await  _readingListService.GetReadingListAsync();

            return Ok(readingList);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> ListItem(int id)
        {
            var readingListItem = await _readingListService.GetReadingListItemById(id);

            if(readingListItem is null)
                return NotFound();

             return Ok(readingListItem);
        }

        //Create

        [HttpPost("add")]

        public async Task<IActionResult> Add(ReadingListCreate model) //maybe create
        {
            if(!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (!await _readingListService.CreateReadingListItemAsync(model))
                return UnprocessableEntity();

            return Ok("Book added to Reading List");

        }

        //Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id) //maybe delete
        {
            var readingListItem = await _readingListService.GetReadingListItemById(id);

            if (readingListItem is null)
                return NotFound();

            if (!await _readingListService.DeleteReadingListItemAsync(id))
                return BadRequest();

            return Ok("Book removed from Reading List");
        }


        //Helper Methods:

        //Helper method to set user Id

        private bool SetUserIdInService()
        {
            var userId = GetUserId();

            if (userId == null)
                return false;

            _readingListService.SetUserId(userId);
            return true;
        }

        //Helper method to get user Id (from auth token sent from API after user logs in)
        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            if (userIdClaim == null)
                return null;

            return userIdClaim;
        }
    }
}
