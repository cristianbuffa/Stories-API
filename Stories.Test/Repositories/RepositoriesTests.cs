using NSubstitute;
using NUnit.Framework;
using Stories.Common.Models;
using Stories.Infrastructure.Repository;
using Stories.SharedKernel.Interfaces;

namespace Stories.Test.Repositories;

public class RepositoriesTests
{
    private IHnClient _hnClient = null!;
    private IStoryRepository _storyRepository = null!;

    [SetUp]
    public void Setup()
    {
        _hnClient = Substitute.For<IHnClient>();

        _storyRepository = new StoryRepository(_hnClient);
    }

    [Test]
    public async Task GetStoriesAsync_Success()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 0, OrderBy = "orderBy" };
        var ids = new List<int>() { 1, 2, 3 };

        _hnClient.GetNewestStoriesAsync(request).Returns(ids);
        _hnClient.GetNewestStoriesDetailsAsync(ids).Returns(new List<GetStoryDetailsResponse?>()
        {
            new()
            {
                Title = "title",
                Url = "url"
            },
            new()
            {
                Title = "title1"
            }
        });

        // Act
        var stories = await _storyRepository.GetStoriesAsync(request);

        // Assert
        Assert.That(stories, Is.Not.Null);
    }

    [Test]
    public async Task GetStoryDetailsByIdAsync_Success()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 0, OrderBy = "orderBy" };

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

}
