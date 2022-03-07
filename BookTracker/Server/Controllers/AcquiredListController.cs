using BookTracker.Server.Services.ListServices;
using BookTracker.Shared.Models.List.AcquiredList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcquiredListController : ControllerBase
    {
        //Field

        private readonly IAcquiredListService _acquiredListService;

        //Constructor

        public AcquiredListController(IAcquiredListService acquiredListService)
        {
            _acquiredListService = acquiredListService;
        }

        //Methods

        //Gets

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var acquiredList = await _acquiredListService.GetAcquiredListAsync();

            return Ok(acquiredList);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> ListItem(int id)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var acquiredListItem = await _acquiredListService.GetAcquiredListItemByIdAsync(id);

            if (acquiredListItem is null)
                return NotFound();

            return Ok(acquiredListItem);
        }

        //Create

        [HttpPost("add")]

        public async Task<IActionResult> Add(AcquiredListCreate model) //maybe create
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (!SetUserIdInService())
                return Unauthorized();

            if (!await _acquiredListService.CreateAcquiredListItemAsync(model))
                return UnprocessableEntity();

            return Ok("Book added to Acquired List");

        }

        //Update
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, AcquiredListEdit model)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            if (!ModelState.IsValid || model == null)
                return BadRequest(ModelState);

            if (model.Id != id)
                return BadRequest();

            if (!await _acquiredListService.UpdateAcquiredListItemAsync(id, model))
                return BadRequest();

            return Ok("List item updated successfully");

        }

        //AddFromReadingList

        [HttpPut("add/readinglist/{id}")]

        public async Task<IActionResult> AddFromReadingList(int id, AcquiredListEdit model)
        {
            if (!SetUserIdInService())
                return Unauthorized();

            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            if (model.Id != id)
                return BadRequest();

            if (!await _acquiredListService.AddToAcquiredListFromReadingAsync(id, model))
                return UnprocessableEntity();

            return Ok("List item moved to Acquired List");


        }

        //Delete

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id) //maybe delete
        {
            if (!SetUserIdInService())
                return Unauthorized();

            var acquiredListItem = await _acquiredListService.GetAcquiredListItemByIdAsync(id);

            if (acquiredListItem is null)
                return NotFound();

            if (!await _acquiredListService.DeleteAcquiredListItemAsync(id))
                return BadRequest();

            return Ok("Book removed from Acquired List");
        }


        //Helper Methods:

        //Helper method to set user Id

        private bool SetUserIdInService()
        {
            var userId = GetUserId();

            if (userId == null)
                return false;

            _acquiredListService.SetUserId(userId);
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
