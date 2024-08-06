using Stories.Domain.Request;
using Stories.Domain.Response;


namespace Stories.Domain.Interface
{
    public interface IHnClient
    {
        Task<List<int>?> GetNewestStoriesAsync(GetStoriesRequest request);

        Task<List<GetStoryDetailsResponse?>> GetNewestStoriesDetailsAsync(List<int> storiesIds);

        Task<GetStoryDetailsResponse?> GetStoryDetailsByIdAsync(int id);

    }
}
