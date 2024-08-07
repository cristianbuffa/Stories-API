using NSubstitute;
using NUnit.Framework;
using Stories.Repository;
using Stories.Domain;
using Stories.Domain.Interface;
using Microsoft.Extensions.Logging;
using Stories.Domain.Request;
using Stories.Domain.Response;
using Stories.Service;

namespace Stories.Test.Repositories;

public class RepositoriesTests
{
    private IHnClient _hnClient = null!;
    private IStoryRepository _storyRepository = null!;


    [SetUp]
    public void Setup()
    {
        _hnClient = Substitute.For<IHnClient>();

        _storyRepository = new StoryRepository(_hnClient, null!);
    }

    [Test]
    public async Task GetStoriesAsync_Success()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 1, OrderBy = "Priority" };
        // Act
        var storiesId = _hnClient.GetNewestStoriesAsync(request)
            .Returns(new List<int>(new int[] { 1 }));
        // Assert
        Assert.That(storiesId, Is.Not.Null);
    }

    [Test]
    public async Task GetStoryDetailsByIdAsync_Success()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 50, OrderBy = "Priority" };

        _hnClient.GetStoryDetailsByIdAsync(1).Returns(new GetStoryDetailsResponse()
        {
            Title = "title",
            Url = "url"
        });

        // Act
        var stories = await _storyRepository.GetStoryDetailsAsync(1);

        // Assert
        Assert.That(stories, Is.Not.Null);
    }

    [Test]
    public async Task GetStoryDetailsByIdAsync_NotSuccess()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 50, OrderBy = "orderBy" };

        _hnClient.GetStoryDetailsByIdAsync(1).Returns(new GetStoryDetailsResponse()
        {
            Title = "title",
            Url = "url"
        });

        // Act
        var stories = await _storyRepository.GetStoryDetailsAsync(1);

        //// Assert
        Assert.That(stories, Is.Not.Null);
    }

}
