using BookTracker.Server.Services.ListServices;
using BookTracker.Shared.Models.List.ReadList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadListController : ControllerBase
    {

        //Field

        private readonly IReadListService _readListService;

        //Constructor

        public ReadListController(IReadListService readListService)
        {
            _readListService = readListService;
        }

        //Methods

        //Gets

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var readList = await _readListService.GetReadListAsync();

            return Ok(readList);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> ListItem(int id)
        {
            var readListItem = await _readListService.GetReadListItemByIdAsync(id);

            if (readListItem is null)
                return NotFound();

            return Ok(readListItem);
        }

        //Create

        [HttpPost("add")]

        public async Task<IActionResult> Add(ReadListCreate model) //maybe create
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (!await _readListService.CreateReadListItemAsync(model))
                return UnprocessableEntity();

            return Ok("Book added to Read List");

        }

        //Update
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ReadListEdit model)
        {
            if (!ModelState.IsValid || model == null)
                return BadRequest(ModelState);

            if (model.Id != id)
                return BadRequest();

            if (!await _readListService.UpdateReadListItemAsync(id, model))
                return BadRequest();

            return Ok("List item updated successfully");

        }

        //AddFromReadingList

        [HttpPut("add/acquiredlist/{id}")]

        public async Task<IActionResult> AddFromAcquiredList(int id, ReadListFromAcquiredEdit model)
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (model.Id != id)
                return BadRequest();

            if (!await _readListService.AddToReadListFromAcquiredAsync(id, model))
                return UnprocessableEntity();

            return Ok("List item moved to Read List");


        }

        //Once add from Reading is built in service, also add here

        //Delete

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id) //maybe delete
        {
            var readListItem = await _readListService.GetReadListItemByIdAsync(id);

            if (readListItem is null)
                return NotFound();

            if (!await _readListService.DeleteReadListItemAsync(id))
                return BadRequest();

            return Ok("Book removed from Read List");
        }


        //Helper Methods:

        //Helper method to set user Id

        private bool SetUserIdInService()
        {
            var userId = GetUserId();

            if (userId == null)
                return false;

            _readListService.SetUserId(userId);
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
