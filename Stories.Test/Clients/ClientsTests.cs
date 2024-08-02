using System.Net;
using NSubstitute;
using NUnit.Framework;
using Stories.Common.Models;
using Stories.Infrastructure.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Stories.Test.Clients;
public class ClientsTests
{
    private IHttpClientFactory _httpClientFactory = null!;
    private HnClient _hnClient = null!;

    [SetUp]
    public void SetUp()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();

        _hnClient = new HnClient(_httpClientFactory);
    }

    [Test]
    public async Task GetStoriesIds_ShouldReturnValidResult()
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpIntListMessageHandler();

        var httpClient = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        // Act
        var result = await _hnClient.GetNewestStoriesAsync(new GetStoriesRequest { Limit = 10, OrderBy = "orderBy" });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            _httpClientFactory.Received(1).CreateClient(Arg.Any<string>());
        });
    }

    [Test]
    public async Task GetStoriesDetails_ShouldReturnValidResult()
    {
        // Arrange
        var request = new List<int> { 1, 2, 3 };
        var mockHttpMessageHandler = new MockHttpStoriesListMessageHandler();

        var httpClient = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        // Act
        var result = await _hnClient.GetNewestStoriesDetailsAsync(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            _httpClientFactory.Received(1).CreateClient(Arg.Any<string>());
        });
    }

    [Test]
    public async Task GetStoryDetailsById_ShouldReturnValidResult()
    {
        // Arrange
        var mockHttpMessageHandler = new MockHttpStoriesListMessageHandler();

        var httpClient = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        _httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        // Act
        var result = await _hnClient.GetStoryDetailsByIdAsync(1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            _httpClientFactory.Received(1).CreateClient(Arg.Any<string>());
        });
    }
}


internal class MockHttpIntListMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(new List<int>()))
        });
    }
}

internal class MockHttpStoriesListMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(new GetStoryDetailsResponse
            {
                Title = "title",
                Url = "url"
            }))
        });
    }
}