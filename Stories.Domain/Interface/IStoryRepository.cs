using Stories.Domain.Request;
using Stories.Domain.Response;

namespace Stories.Domain.Interface
{
    public interface IStoryRepository
    {
        Task<List<GetStoryDetailsResponse?>> GetStoriesAsync(GetStoriesRequest request);
        Task<GetStoryDetailsResponse?> GetStoryDetailsAsync(int id);
    }

}

