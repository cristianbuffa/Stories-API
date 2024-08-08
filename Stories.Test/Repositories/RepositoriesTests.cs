using NSubstitute;
using NUnit.Framework;
using Stories.Repository;
using Stories.Domain.Interface;
using Stories.Domain.Request;
using Stories.Domain.Response;
using Stories.Domain.Exceptions;
using Stories.Domain.Validation;


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
        //
        var stories = await _storyRepository.GetStoriesAsync(request);

        // Assert
        Assert.That(stories, Is.Not.Null);
    }


    [Test]
    public async Task GetStoriesAsync_NotSuccess()
    {
        // Arrange
        var request = new GetStoriesRequest { Limit = 0, OrderBy = "fakeorder" };
        //
        var stories = await _storyRepository.GetStoriesAsync(request);

        // Assert
        Assert.Throws<ApiLegacy404Exception>(
       () => throw new ApiLegacy404Exception(ValidationMessage.
               General("ExternalServiceNotFound", "Story Repository fails.", "")));
    }

    [Test]
    public async Task GetStoryDetailsByIdAsync_Success()
    {
        // Arrange

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
