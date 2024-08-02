using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using NUnit.Framework;
using Stories.Common.Models;
using Stories.Core.Services;
using Stories.SharedKernel.Interfaces;

namespace Stories.Test.Services;
public class ServicesTests
{
    private IStoryRepository _storyRepository = null!;
    private IMemoryCache _cache = null!;
    private StoryService _storyService = null!;
    [SetUp]
    public void Setup()
    {
        _storyRepository = Substitute.For<IStoryRepository>();
        _cache = Substitute.For<IMemoryCache>();

        _storyService = new StoryService(_storyRepository, _cache);
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
        var request = new GetStoriesRequest { Limit = 0, OrderBy = "orderBy" };

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
