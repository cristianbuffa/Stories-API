using System.Net.Http;
using System.Net.Http.Json;
using Stories.Domain;

namespace Stories.Infrastructure;
public class HnClient : IHnClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public const string ClientName = "StoriesApiClient";

    public HnClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<int>?> GetNewestStoriesAsync(GetStoriesRequest request)
    {
        var client = _httpClientFactory.CreateClient(ClientName);
        var uri = $"newstories.json?print=pretty&orderBy=\"${request.OrderBy}\"&limitToFirst={request.Limit}";

        var response = await client.GetAsync(
            $"newstories.json?print=pretty&orderBy=\"${request.OrderBy}\"&limitToFirst={request.Limit}");


        var storiesIds = await response.Content.ReadFromJsonAsync<List<int>>();

        return storiesIds;
    }

    public async Task<List<GetStoryDetailsResponse?>> GetNewestStoriesDetailsAsync(List<int> storiesIds)
    {
        List<GetStoryDetailsResponse?> stories = new();

        var client = _httpClientFactory.CreateClient(ClientName);

        var tasks = storiesIds.Select(async storyId =>
        {
            var detailResponse = await client.GetAsync($"item/{storyId}.json?print=pretty");
            var detail = await detailResponse.Content.ReadFromJsonAsync<GetStoryDetailsResponse>();
            detail!.id = storyId;
            return detail;
        });

        var details = await Task.WhenAll(tasks);

        stories.AddRange(details.Where(detail => detail != null));

        return stories;
    }

    public async Task<GetStoryDetailsResponse?> GetStoryDetailsByIdAsync(int id)
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync($"item/{id}.json?print=pretty");

        GetStoryDetailsResponse? storyDetails = await response.Content.ReadFromJsonAsync<GetStoryDetailsResponse>();

        return storyDetails;
    }

    public async Task<List<GetUsersDetailsResponse?>> GetUsersDetailsAsync()
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync($"user/jl.json?print=pretty");

        var usersDetails = await response.Content.ReadFromJsonAsync<List<GetUsersDetailsResponse>>();

        return usersDetails!;
        //https://hacker-news.firebaseio.com/v0/user/jl.json?print=pretty
    }
}