using System.Net.Http.Json;
using Stories.Domain.Exceptions;
using Stories.Domain.Interface;
using Stories.Domain.Request;
using Stories.Domain.Response;
using Stories.Domain.Validation;

namespace Stories.Infrastructure;
public class HnClient : IHnClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    public const string ClientName = "StoriesApiClient";

    public HnClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;    }

    public async Task<List<int>?> GetNewestStoriesAsync(GetStoriesRequest request)
    {
        var client = _httpClientFactory.CreateClient(ClientName);
        var response = await client.GetAsync(
            $"newstories.json?print=pretty&orderBy=\"${request.OrderBy}\"&limitToFirst={request.Limit}");

        if ((int)response.StatusCode != 200)
        {
            throw new ApiLegacy404Exception(ValidationMessage.
               General("ExternalServiceNotFound", "External Service fails.", null));
        }
        var storiesIds = await response.Content.ReadFromJsonAsync<List<int>>();
        return storiesIds;
  
    }
    public async Task<List<GetStoryDetailsResponse?>> GetNewestStoriesDetailsAsync(List<int> storiesIds)
    {
        
        using (var client = _httpClientFactory.CreateClient(ClientName))
        {
            List<GetStoryDetailsResponse?> stories = new();
            var tasks = storiesIds.Select(async storyId =>
            {
                var detail = await GetStoryDetailsByIdAsync(storyId);
                return detail;
            });
            var details = await Task.WhenAll(tasks);
            stories.AddRange(details.Where(detail => detail != null));
            return stories;
        } 
    }

    public async Task<GetStoryDetailsResponse?> GetStoryDetailsByIdAsync(int id)
    {
        using (var client = _httpClientFactory.CreateClient(ClientName))
        {
            var response = await client.GetAsync($"item/{id}.json?print=pretty");

            if ((int)response.StatusCode != 200)
            {
                throw new ApiLegacy404Exception(ValidationMessage.
                   General("ExternalServiceNotFound", "External Service fails.", null));
            }
            GetStoryDetailsResponse? storyDetails = await response.Content.ReadFromJsonAsync<GetStoryDetailsResponse>();
            return storyDetails;
        }

    }

}