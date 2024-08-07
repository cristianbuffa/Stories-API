using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using NUnit.Framework;
using Stories.Domain.Interface;
using Stories.Domain.Request;
using Stories.Domain.Response;
using Stories.Domain.Validation;
using Stories.Service;


namespace Stories.Test.Services;
public class ServicesTests
{
    private IStoryRepository _storyRepository = null!;
    private IMemoryCache _cache = null!;
    private IDomainModelValidator _domainModelValidator = null!;
    private StoryService _storyService = null!;
    [SetUp]
    public void Setup()
    {
        _storyRepository = Substitute.For<IStoryRepository>();
        _cache = Substitute.For<IMemoryCache>();
        _domainModelValidator = Substitute.For<IDomainModelValidator>();

        _storyService = new StoryService(_storyRepository, _cache, _domainModelValidator);
    }

    [TearDown]
    public void TearDown()
    {
        _cache.Dispose();
    }

    [Test]
    public async Task GetStoriesAsync_Success()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 50, OrderBy = "Priority" };

        _storyRepository.GetStoriesAsync(request)
            .Returns(new List<GetStoryDetailsResponse?>()
            {
                new()
                {
                    Title = "Title",
                    Url = "Url"
                },
                new()
                {
                    Title = "Title2"
                }
            });

        // Act
        var stories = await _storyService.GetStoriesAsync(request);

        // Assert
        Assert.That(stories, Is.Not.Null);
    }

    [Test]
    public async Task GetStoryDetails_Success()
    {
        // Arrange

        _storyRepository.GetStoryDetailsAsync(123)
            .Returns(new GetStoryDetailsResponse()
            {
                Title = "Title",
                Url = "Url"
            });

        // Act
        var stories = await _storyService.GetStoryDetailsAsync(123);

        // Assert
        Assert.That(stories, Is.Not.Null);
    }
}
