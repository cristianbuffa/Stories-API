using Microsoft.AspNetCore.Mvc;
using Stories.Domain.Interface;
using Stories.Domain.Request;


namespace Stories.API;


[Route("stories-api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    return Ok();
    //}

    [HttpGet("GetStories")]
    [ValidateModelState]
    public async Task<IActionResult> GetStories([FromQuery] GetStoriesRequest request)
    {
        var response = await _storyService.GetStoriesAsync(request);
        return Ok(response);
    }

    [HttpGet("GetStory")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)] 
    public async Task<IActionResult> GetStoryById([FromQuery] int id)
    {
        var response = await _storyService.GetStoryDetailsAsync(id);
        return Ok(response);
    }

}
