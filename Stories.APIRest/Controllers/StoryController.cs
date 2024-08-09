using Microsoft.AspNetCore.Mvc;
using Stories.Domain.Interface;
using Stories.Domain.Request;
using Stories.Domain.Response;


namespace Stories.API;


[Route("stories-api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpGet("GetStories")]
    [ValidateModelState]
    [ProducesResponseType(typeof(List<GetStoryDetailsResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStories([FromQuery] GetStoriesRequest request)
    {
        var response = await _storyService.GetStoriesAsync(request);
        return Ok(response);
    }

    [HttpGet("GetStory")]
    [ProducesResponseType(typeof(GetStoryDetailsResponse), StatusCodes.Status200OK)] 
    public async Task<IActionResult> GetStoryById([FromQuery] int id)
    {
        var response = await _storyService.GetStoryDetailsAsync(id);
        return Ok(response);
    }

}
